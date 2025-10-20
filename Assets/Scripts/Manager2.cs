using UnityEngine;
using System.Collections.Generic;


public class Manager2: MonoBehaviour
{
    public void doSomething()
    {
        int[] arr = new int[1024];
        for (int i = 0; i < arr.Length; i++) arr[i] = Random.Range(0, 100);
        System.Array.Sort(arr);
        int s = 0; for (int i = 0; i < arr.Length; i++) s += arr[i];
    }


    public void Process()
    {
        doSomething();
    }
}