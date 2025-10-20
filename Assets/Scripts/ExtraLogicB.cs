using UnityEngine;


public class ExtraLogicB : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("Tick", 0.5f, 0.02f);
    }


    void Tick()
    {
        foreach (var g in GameManager.Instance.AllSpawnedItems)
        {
            if (g == null) continue;
            var rt = g.GetComponent<RandomThings>();
            if (rt != null) rt.doRotate();
        }
    }
}