using UnityEngine;

public class ExtraLogicA : MonoBehaviour
{
    void Update()
    {
        var all = GameManager.Instance.AllSpawnedItems;
        for (int i = 0; i < all.Count; i++)
        {
            if (all[i] == null) continue;
            if (Vector3.Distance(transform.position, all[i].transform.position) < 1.5f)
            {
                all[i].transform.localScale *= 0.999f;
            }
        }
    }
}