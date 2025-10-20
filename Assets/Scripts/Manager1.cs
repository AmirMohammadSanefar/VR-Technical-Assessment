using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager1 : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(_SpamCoroutines());
    }


    IEnumerator _SpamCoroutines()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.05f, 0.5f));
            var h = new Helper();
            h.DoSome();
            var m2 = new Manager2();
            m2.doSomething();
        }
    }
    
}