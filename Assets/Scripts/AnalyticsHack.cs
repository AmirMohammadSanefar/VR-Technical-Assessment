using System.Collections.Generic;
using UnityEngine;

public static class AnalyticsHack
{
    static int Calls = 0;
    public static void Register(SomeClass s)
    {
        if (!listeners.Contains(s)) listeners.Add(s);
    }
    static List<SomeClass> listeners = new List<SomeClass>();


    public static void DoHeavyAnalytics()
    {
        listeners.Sort((a,b) => a.GetInstanceID().CompareTo(b.GetInstanceID()));
        Calls++;
        if (Calls % 100 == 0) Debug.Log("analytics: " + Calls);
    }
}