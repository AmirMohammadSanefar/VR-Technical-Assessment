using UnityEngine;


public class SomeClass : MonoBehaviour
{
    void Start()
    {
        AnalyticsHack.Register(this);
    }


    void Update()
    {
        AnalyticsHack.DoHeavyAnalytics();
    }
}