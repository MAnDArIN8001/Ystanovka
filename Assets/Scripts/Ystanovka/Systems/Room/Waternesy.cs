using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ystanovka.Systems.Room
{
    public class Waternesy : MonoBehaviour
    {
        private bool _isRunning;
        
        private float _currentWaternesy;
        [SerializeField, Min(0)] private float _maxWaternesy = 90;

        [Header("Range")] 
        [SerializeField, Min(0)] private float _minWaternesyRange;
        [SerializeField] private float _maxWaternesyRange;

        [Space, SerializeField] private TMP_Text _waternesyView;

        private Coroutine _wateringCoroutine;

        public float CurrentWaternesy => _currentWaternesy;

        private void Start()
        {
            _currentWaternesy = Random.Range(_minWaternesyRange, _maxWaternesyRange);

            _waternesyView.text = $"Влажность: {_currentWaternesy:F1}%";
        }

        public void StartWatering()
        {
            _isRunning = true;

            _wateringCoroutine = StartCoroutine(Watering());
        }

        public void Stop()
        {
            _isRunning = false;

            if (_wateringCoroutine is not null)
            {
                StopCoroutine(_wateringCoroutine);

                _wateringCoroutine = null;
            }
        }

        private IEnumerator Watering()
        {
            var timer = new WaitForSeconds(0.3f);

            while (_isRunning && _currentWaternesy < _maxWaternesy)
            {
                var randomIncrease = Random.Range(0.01f, 0.05f);

                _currentWaternesy += randomIncrease;

                _waternesyView.text = $"Влажность: {_currentWaternesy:F1}%";

                yield return timer;
            }
        }
    }
}