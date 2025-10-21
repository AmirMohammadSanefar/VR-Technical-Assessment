using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    #region Definitions

    #region Variables

    private static AnalyticsManager _instance;
    private List<AnalyticsComponent> _listeners = new();
    private int callCount = 0;
    private const int LOG_INTERVAL = 100;

    #endregion

    #region Properties

    public static AnalyticsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AnalyticsManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("AnalyticsManager");
                    _instance = go.AddComponent<AnalyticsManager>();
                    DontDestroyOnLoad(go); // برای بقا بین صحنه‌ها
                }
            }
            return _instance;
        }
    }

    #endregion

    #endregion

    #region MonoBehaviourFunctions

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        _listeners.Clear();

        if (_instance == this)
        {
            _instance = null;
        }
    }

    #endregion

    #region Functions

    public void Register(AnalyticsComponent component)
    {
        if (component == null) return;

        if (!_listeners.Contains(component))
        {
            _listeners.Add(component);
            Debug.Log($"AnalyticsComponent registered: {component.gameObject.name}");
        }
    }

    public void Unregister(AnalyticsComponent component)
    {
        if (component != null)
        {
            _listeners.Remove(component);
            Debug.Log($"AnalyticsComponent unregistered: {component.gameObject.name}");
        }
    }

    public void ExecuteAnalytics()
    {
        if (_listeners.Count == 0)
            return;

        _listeners.Sort((a, b) => a.GetInstanceID().CompareTo(b.GetInstanceID()));

        callCount++;

        foreach (var component in _listeners)
        {
            if (component != null)
            {
                ProcessComponentAnalytics(component);
            }
        }

        if (callCount % LOG_INTERVAL == 0)
        {
            Debug.Log($"Analytics calls: {callCount}, Active listeners: {_listeners.Count}");
        }
    }

    private void ProcessComponentAnalytics(AnalyticsComponent component)
    {
        string analyticsData = component.GetAnalyticsData();
    }

    public int GetActiveListenersCount() => _listeners.Count;
    public int GetTotalCallsCount() => callCount;

    #endregion
}
