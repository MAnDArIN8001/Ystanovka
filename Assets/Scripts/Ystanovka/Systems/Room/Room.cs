using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ystanovka.Systems.Room
{
    public class Room : MonoBehaviour
    {
        private bool _isPositiv;
        
        [SerializeField] private TMP_Text temperatureText;

        private const float AirSpecificHeatCapacity = 1200f;
        private const float RoomVolume = 40f;
        private const float ThermalInertiaFactor = 3f;
        private const float InternalHeatSources = 60f;
        private const float LeakageCoefficient = 60f;
        private const float OutsideTemperatureMin = 20f;
        private const float OutsideTemperatureMax = 32f;
        [SerializeField] private float InitialTemperatureMin = 20f;
        [SerializeField] private float InitialTemperatureMax = 30f;

        private float _currentTemperature;
        private float _initTemperature;
        private float _targetTemperature;
        private float _coolingPower;
        private float _effectiveHeatCapacity;
        private float _outsideTemperature;
        private float _elapsedSimulatedMinutes;
        private bool _isRunning;
        private float _timeToReachTargetTemperature;
        private float _simulationTime;

        public float InitTemperature => _initTemperature;
        public float CurrentTemperature => _currentTemperature;
        public float OutsideTemperature => Random.Range(OutsideTemperatureMin, OutsideTemperatureMax);
        public float SimulationTime => _simulationTime;
        public float TimeToReach => _timeToReachTargetTemperature;

        public event Action<float> OnTargetTemperatureReached;
        public event Action<float> OnTimerCompleted;

        private void Start()
        {
            _effectiveHeatCapacity = AirSpecificHeatCapacity * RoomVolume * ThermalInertiaFactor;
            _initTemperature = UnityEngine.Random.Range(InitialTemperatureMin, InitialTemperatureMax);

            _currentTemperature = _initTemperature;

            temperatureText.text = $"Temperature: {_currentTemperature:F1} °C";
        }

        private void Update()
        {
            if (!_isRunning)
            {
                return;
            }

            Simulate(Time.deltaTime);
            UpdateTemperatureText();
        }

        public void StartTemperatureChange(float targetTemperature, float energyConsumptionWatts,
            float timeToReachTarget)
        {
            _targetTemperature = targetTemperature;
            _coolingPower = energyConsumptionWatts * 2.5f;
            _outsideTemperature = 19f;
            _elapsedSimulatedMinutes = 0f;
            _isRunning = true;

            _timeToReachTargetTemperature = timeToReachTarget;
            _simulationTime = 0f;

            _isPositiv = targetTemperature >= _currentTemperature;
            
            Debug.Log(_isPositiv);
        }

        private void Simulate(float realDeltaTimeSeconds)
        {
            float simulatedDeltaMinutes = realDeltaTimeSeconds;
            float simulatedDeltaSeconds = simulatedDeltaMinutes * 60f;

            float heatGain = InternalHeatSources + LeakageCoefficient * (_outsideTemperature - _currentTemperature);
            float netPower;

            if (_targetTemperature < _currentTemperature)
            {
                netPower = _coolingPower - heatGain/3;
            }
            else
            {
                netPower = heatGain;
            }

            float temperatureChangePerSecond = netPower / _effectiveHeatCapacity;
            temperatureChangePerSecond *= _isPositiv ? 1 : -1;
            
            Debug.Log($"{temperatureChangePerSecond} {netPower} {_isPositiv} {_effectiveHeatCapacity}");
            
            float deltaTemperature = temperatureChangePerSecond * simulatedDeltaSeconds;
            _currentTemperature += deltaTemperature;
            _elapsedSimulatedMinutes += simulatedDeltaMinutes;

            if ((_isPositiv && _currentTemperature >= _targetTemperature) ||
                (!_isPositiv && _currentTemperature <= _targetTemperature))
            {
                _currentTemperature = _targetTemperature;
                _isRunning = false;
                OnTargetTemperatureReached?.Invoke(Mathf.Abs(_targetTemperature - _currentTemperature));
            }

            _simulationTime += simulatedDeltaMinutes;
            if (_simulationTime >= _timeToReachTargetTemperature)
            {
                OnTimerCompleted?.Invoke(_timeToReachTargetTemperature);
                _isRunning = false;
            }
        }

        private void UpdateTemperatureText()
        {
            temperatureText.text = $"Temperature: {_currentTemperature:F1} °C\n";
        }
    }
}