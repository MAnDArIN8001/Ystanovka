using System.Collections.Generic;
using UI.PopUp;
using UI.Table.Row;
using UnityEngine;
using Ystanovka.Controls;
using Ystanovka.Controls.ControlsButton;
using Ystanovka.Systems;
using Ystanovka.Systems.Room;
using Ystanovka.Systems.StateView;

namespace Ystanovka
{
    public class Conder : MonoBehaviour
    {
        private bool _power = false;
        private bool _active = false;
        
        [SerializeField] private ControlButton _powerButton;
        [SerializeField] private ControlButton _activeButton;
        
        [Header("Systems")] 
        [SerializeField] private ValueSystem _temperatureSystem;
        [SerializeField] private ValueSystem _timerSystem;
        [SerializeField] private StateSystem _waternesySystem;
        [SerializeField] private StateSystem _strengthSystem;
        [SerializeField] private StateSystem _directionSysetm;

        [Header("Dependensys")] 
        [SerializeField] private Room _room;
        [SerializeField] private Waternesy _waternesy;

        [Space, SerializeField] private TimeEndPopUp _timePopUp;
        [SerializeField] private TemperatureEndPopUp _temperatruePopUp;

        [Space, SerializeField] private Rotator _upRotator;
        [SerializeField] private Rotator _downRotator;
        [SerializeField] private Rotator _airRotator;
        [SerializeField] private Rotator _waterRotator;

        [Space, SerializeField] private AudioSource _airSound;

        [Space, SerializeField] private ParticleSystem _airParticles;
        [SerializeField] private ParticleSystem _waterParticles;

        [Space, SerializeField] private TableController _table;

        [Space, SerializeField] private List<float> _speeds;

        public bool Power => _power;

        private void OnEnable()
        {
            if (_powerButton is not null)
            {
                _powerButton.OnButtonPressed += HandlePowerPressed;
            }

            if (_activeButton is not null)
            {
                _activeButton.OnButtonPressed += HandleActivation;
            }

            if (_room is not null)
            {
                _room.OnTimerCompleted += HandleTimeEnd;
                _room.OnTargetTemperatureReached += HandleTemperature;
            }
        }

        private void OnDisable()
        {
            if (_powerButton is not null)
            {
                _powerButton.OnButtonPressed -= HandlePowerPressed;
            }
            
            if (_activeButton is not null)
            {
                _activeButton.OnButtonPressed -= HandleActivation;
            }
            
            if (_room is not null)
            {
                _room.OnTimerCompleted -= HandleTimeEnd;
                _room.OnTargetTemperatureReached -= HandleTemperature;
            }
        }

        private void HandlePowerPressed()
        {
            _power = !_power;
            
            if (_power)
            {
                _timerSystem.Enable();
                _temperatureSystem.Enable();
                _waternesySystem.Enable();
                _strengthSystem.Enable();
                _directionSysetm.Enable();
            }
            else
            {
                _timerSystem.Disable();
                _temperatureSystem.Disable();
                _waternesySystem.Disable();
                _strengthSystem.Disable();
                _directionSysetm.Disable();
            }
        }

        private void HandleActivation()
        {
            if (!_power || _active)
            {
                return;
            }
            
            _timerSystem.Disable();
            _temperatureSystem.Disable();
            _waternesySystem.Disable();
            _strengthSystem.Disable();
            _directionSysetm.Disable();
            
            _airSound.Play();
            
            _upRotator.RotateToValue(_directionSysetm.CurrentIndex);
            _downRotator.RotateToValue(_directionSysetm.CurrentIndex);
            _airRotator.RotateToValue(_directionSysetm.CurrentIndex);
            _waterRotator.RotateToValue(_directionSysetm.CurrentIndex);

            var shouldWatering = _waternesySystem.CurrentIndex == 1;

            var main = _airParticles.main;
            main.startSpeed = _speeds[_strengthSystem.CurrentIndex];

            if (shouldWatering)
            {
                _waternesy.StartWatering();
                _waterParticles.Play();
            }
            
            _airParticles.Play();

            _active = true;

            var time = _timerSystem.CurrentValue * 60;
            var power = GetPower(_strengthSystem.CurrentIndex);
            var temperature = _temperatureSystem.CurrentValue;
            
            _room.StartTemperatureChange(temperature, power, time);
        }

        private void Deactivate()
        {
            _active = false;
            
            _timerSystem.Enable();
            _temperatureSystem.Enable();
            _waternesySystem.Enable();
            _strengthSystem.Enable();
            _directionSysetm.Enable();
            
            _waternesy.Stop();
            _airSound.Stop();
            _airParticles.Stop();
            _waterParticles.Stop();
        }

        private void HandleTimeEnd(float time)
        {
            _waternesy.Stop();

            var rowData = GetData();
            
            _table.AddRow(rowData);
            
            _timePopUp.Show(time);
            
            Deactivate();
        }

        private void HandleTemperature(float temperature)
        {
            _waternesy.Stop();

            var rowData = GetData();
            
            _table.AddRow(rowData);
            
            _temperatruePopUp.Show(_room.SimulationTime, _room.TimeToReach - _room.SimulationTime);
            
            Deactivate();
        }

        private RowData GetData()
        {
            var rowData = new RowData();

            rowData.StartT = _room.InitTemperature;
            rowData.EndT = _room.CurrentTemperature;
            rowData.Power = GetPower(_strengthSystem.CurrentIndex);
            rowData.Time = _room.SimulationTime;
            rowData.OutsideT = _room.OutsideTemperature;
            rowData.DeltaT = -(rowData.StartT - rowData.EndT);
            rowData.Waternesy = _waternesy.CurrentWaternesy;

            return rowData;
        }

        private int GetPower(int powerCof)
        {
            var power = powerCof switch
            {
                0 => 200,
                1 => 300,
                2 => 400,
                _ => 0
            };

            return power;
        }
    }
}