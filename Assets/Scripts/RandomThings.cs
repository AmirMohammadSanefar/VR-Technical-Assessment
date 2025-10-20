using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomThings : MonoBehaviour
{
    public GameManager main;
    public bool taken = false;
    private MeshRenderer rend;
    private Color originalColor;
    private Material instanceMat;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        instanceMat = new Material(rend.material);
        rend.material = instanceMat;
        rend.material.color = Color.white;
        var c = Random.Range(0f, 1f);
        if (c >= 0.5f)
        {
            originalColor = Color.red;
        }
        else
        {
            originalColor = Color.green;
        }
    }


    private void OnCollisionEnter(Collision c)
    {
        if (taken) return;
        if (c.gameObject.CompareTag("Player"))
        {
            taken = true;
            AnotherManager._CollectedCount++;
            if (originalColor == Color.red)
            {
                AnotherManager._CollectedCount -= 2;
            }

            //GameManager.Instance.AllSpawnedItems.Remove(gameObject);
            UtilityStuff._Put("lastcol", Time.time);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        //if (main != null && main._playerLights != null && main._playerLights[0] != null && main._playerLights[1] != null)
        //{
        //    Gizmos.color = originalColor;

        //    float rayLength = 5f;
        //    float radius = 0.2f;

        //    Vector3 start0 = main._playerLights[0].transform.position;
        //    Vector3 end0 = start0 + main._playerLights[0].transform.forward * rayLength;
        //    Gizmos.DrawLine(start0, end0);
        //    Gizmos.DrawWireSphere(start0, radius);
            
        //    Vector3 start1 = main._playerLights[1].transform.position;
        //    Vector3 end1 = start1 + main._playerLights[1].transform.forward * rayLength;
        //    Gizmos.DrawLine(start1, end1);
        //    Gizmos.DrawWireSphere(start1, radius);
        //}
    }


    void Update()
    {
        //transform.Rotate(Vector3.up * Random.Range(10, 360) * Time.deltaTime);
        //if (main != null && main._playerLights != null && rend != null)
        //{
        //    Ray ray0 = new Ray(main._playerLights[0].transform.position, main._playerLights[0].transform.forward);
        //    RaycastHit hit0;
        //    float radius0 = 1f; 

        //    if (Physics.SphereCast(ray0, radius0, out hit0, 5f))
        //    {
        //        if (hit0.collider.gameObject == gameObject)
        //        {
        //            hit0.collider.gameObject.GetComponent<RandomThings>().rend.material.color = originalColor;
        //            return;
        //        }
        //    }

        //    Ray ray1 = new Ray(main._playerLights[1].transform.position, main._playerLights[1].transform.forward);
        //    RaycastHit hit1;
        //    float radius1 = 1f; 

        //    if (Physics.SphereCast(ray1, radius1, out hit1, 5f))
        //    {
        //        if (hit1.collider.gameObject == gameObject)
        //        {
        //            hit1.collider.gameObject.GetComponent<RandomThings>().rend.material.color = originalColor;
        //            return;
        //        }
        //    }
        //}
        rend.material.color = Color.white;
    }

    public void doRotate()
    {
        transform.Rotate(0, 30 * Time.deltaTime, 0);
    }
}