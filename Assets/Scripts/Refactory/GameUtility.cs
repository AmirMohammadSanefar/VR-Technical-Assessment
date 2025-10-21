using System.Collections.Generic;
using UnityEngine;

public static class GameUtility
{
    #region Definitions

    #region Variables

    private static Dictionary<string, object> _dataBag = new();

    #endregion

    #endregion

    #region Functions

    public static void Put(string key, object value)
    {
        if (_dataBag.ContainsKey(key))
            _dataBag[key] = value;
        else
            _dataBag.Add(key, value);
    }

    public static object Get(string key)
    {
        return _dataBag.ContainsKey(key) ? _dataBag[key] : null;
    }

    public static T Get<T>(string key, T defaultValue = default(T))
    {
        object value = Get(key);
        return value is T typedValue ? typedValue : defaultValue;
    }

    public static void ClearAll()
    {
        foreach (var item in GameManager.AllCollectibleItems)
        {
            if (item != null)
            {
                Object.Destroy(item);
            }
        }
        GameManager.AllCollectibleItems.Clear();
        _dataBag.Clear();
    }

    #endregion
}
