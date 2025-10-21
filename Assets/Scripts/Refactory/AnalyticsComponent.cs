using UnityEngine;

namespace Refactory
{
    public class AnalyticsComponent : MonoBehaviour
    {
        #region MonoBehaviourFunctions

        private void Start()
        {
            RegisterWithAnalytics();
        }

        private void OnEnable()
        {
            RegisterWithAnalytics();
        }

        private void OnDisable()
        {
            UnregisterFromAnalytics();
        }

        private void OnDestroy()
        {
            UnregisterFromAnalytics();
        }

        private void Update()
        {
            AnalyticsManager.Instance.ExecuteAnalytics();
        }

        #endregion

        #region Functions

        private void RegisterWithAnalytics()
        {
            if (AnalyticsManager.Instance != null)
            {
                AnalyticsManager.Instance.Register(this);
            }
        }

        private void UnregisterFromAnalytics()
        {
            if (AnalyticsManager.Instance != null)
            {
                AnalyticsManager.Instance.Unregister(this);
            }
        }

        public string GetAnalyticsData()
        {
            return $"AnalyticsComponent_{gameObject.name}_{GetInstanceID()}";
        }

        public void SendCustomEvent(string eventName, params object[] data)
        {
            Debug.Log($"Analytics Event: {eventName} from {gameObject.name}");
        }

        #endregion
    }
}
