using System.Collections;
using UnityEngine;

public class GameplaySystem : MonoBehaviour
{
    #region MonoBehaviourFunctions

    private void Start()
    {
        StartCoroutine(ProximityCheckCoroutine());
        StartCoroutine(BatchRotationCoroutine());
    }

    #endregion

    #region Functions

    private IEnumerator ProximityCheckCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            CheckProximityScaling();
        }
    }

    private IEnumerator BatchRotationCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            ProcessBatchRotation();
        }
    }

    private void CheckProximityScaling()
    {
        foreach (var item in GameManager.AllCollectibleItems)
        {
            if (item == null) continue;

            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < 1.5f)
            {
                item.transform.localScale *= 0.999f;
            }
        }
    }

    private void ProcessBatchRotation()
    {
        foreach (var item in GameManager.AllCollectibleItems)
        {
            if (item == null) continue;

            var collectible = item.GetComponent<CollectibleItem>();
            collectible?.DoRotate();
        }
    }

    #endregion
}
