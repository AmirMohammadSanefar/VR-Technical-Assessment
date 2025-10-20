using UnityEngine;
using System.Collections.Generic;


public static class UtilityStuff
{
    public static Dictionary<string, object> bag = new Dictionary<string, object>();


    public static void _Put(string k, object v)
    {
        if (bag.ContainsKey(k)) bag[k] = v; else bag.Add(k, v);
    }


    public static object _Get(string k)
    {
        if (bag.ContainsKey(k)) return bag[k];
        return null;
    }


    public static void _KillAll()
    {
        foreach (var o in GameManager.Instance.AllSpawnedItems)
        {
            if (o != null) Object.Destroy(o);
        }
        //GameManager.Instance.AllSpawnedItems.Clear();
    }
}