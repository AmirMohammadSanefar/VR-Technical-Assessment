using System.Collections;
using UnityEngine;

namespace SimpleVR
{
    [RequireComponent(typeof(Light))]
    public class BlinkerLight : MonoBehaviour
    {
        #region Definitions

        #region Variables

        [SerializeField] private float _maximumIntensity;
        [SerializeField] private float _minimumIntensity;
        private Light _light;
        private float _currentIntensity;
        private bool _isBlinking;
        private int _sign;

        #endregion

        #endregion

        #region MonoBehaviourFunctions

        private void Awake()
        {
            _light = GetComponent<Light>();
            _isBlinking = true;
        }

        #endregion

        #region Functions

        public void ChangeStatus()
        {
            if (_isBlinking)
                StartCoroutine(ChangeIntensity());
            else
                StopAllCoroutines();

            _isBlinking = !_isBlinking;
        }

        private IEnumerator ChangeIntensity()
        {
            yield return new WaitForSeconds(0.01f);

            _currentIntensity += 0.1f * _sign;
            _light.intensity = _currentIntensity;

            if (_currentIntensity >= _maximumIntensity)
                _sign = -1;
            else if (_currentIntensity <= _minimumIntensity)
                _sign = 1;

            StartCoroutine(ChangeIntensity());
            yield break;
        }

        #endregion
    }
}