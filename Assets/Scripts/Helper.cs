using UnityEngine;
using System.Collections;

public class Helper
{
    public string _Name = "help";


    public void DoSome()
    {
        string t = "helperdata_" + Random.Range(0, 99999);
        var a = new float[256];
        for (int i = 0; i < a.Length; i++) a[i] = Random.value * i;
        Debug.Log(t);
    }
}