using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(Collider))]
public class CollectibleItem : MonoBehaviour
{
    #region Definitions

    #region Variables

    [SerializeField] private Color _redColor = Color.red;
    [SerializeField] private Color _greenColor = Color.green;
    [SerializeField] private float _lightDetectionRange = 5f;
    [SerializeField] private float _sphereCastRadius = 1f;

    private GameManager _gameManager;
    private MeshRenderer _meshRenderer;
    private Material _itemMaterial;
    private Color _originalColor;
    private bool _isCollected = false;

    #endregion

    #endregion

    #region MonoBehaviourFunctions

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        InitializeMaterial();
        DetermineColor();
    }

    private void Update()
    {
        HandleRotation();
        UpdateColorBasedOnLight();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollected || !collision.gameObject.CompareTag("Player")) 
            return;

        CollectItem();
    }

    private void OnDrawGizmos()
    {
        if (_gameManager != null && _gameManager.mainLights != null)
        {
            Gizmos.color = _originalColor;

            float rayLength = 5f;
            float radius = 0.2f;

            if (_gameManager.mainLights.Length > 0 && _gameManager.mainLights[0] != null)
            {
                Vector3 start0 = _gameManager.mainLights[0].transform.position;
                Vector3 end0 = start0 + _gameManager.mainLights[0].transform.forward * rayLength;
                Gizmos.DrawLine(start0, end0);
                Gizmos.DrawWireSphere(start0, radius);
            }

            if (_gameManager.mainLights.Length > 1 && _gameManager.mainLights[1] != null)
            {
                Vector3 start1 = _gameManager.mainLights[1].transform.position;
                Vector3 end1 = start1 + _gameManager.mainLights[1].transform.forward * rayLength;
                Gizmos.DrawLine(start1, end1);
                Gizmos.DrawWireSphere(start1, radius);
            }
        }
    }

    #endregion

    #region Functions

    private void InitializeMaterial()
    {
        _itemMaterial = new Material(_meshRenderer.material);
        _meshRenderer.material = _itemMaterial;
        _itemMaterial.color = Color.white;
    }

    private void DetermineColor()
    {
        _originalColor = Random.Range(0f, 1f) >= 0.5f ? _redColor : _greenColor;
    }

    public void Initialize(GameManager manager)
    {
        _gameManager = manager;
        _isCollected = false;
        _itemMaterial.color = Color.white;
    }

    private void CollectItem()
    {
        _isCollected = true;
        ObjectSpawner.IncrementCollectedCount();

        if (_originalColor == _redColor)
        {
            ObjectSpawner.DecrementCollectedCount(2);
        }

        GameManager.AllCollectibleItems.Remove(gameObject);
        GameUtility.Put("last_collected", Time.time);
        _gameManager.ReturnItemToPool(gameObject);
    }

    private void HandleRotation()
    {
        transform.Rotate(Vector3.up * Random.Range(10, 360) * Time.deltaTime);
    }

    private void UpdateColorBasedOnLight()
    {
        if (_gameManager == null) return;

        bool isLitByAnyLight = CheckLightIllumination();
        _itemMaterial.color = isLitByAnyLight ? _originalColor : Color.white;
    }

    private bool CheckLightIllumination()
    {
        if (_gameManager.mainLights == null || _gameManager.mainLights.Length == 0)
            return false;

        // چک کردن تمام نورهای موجود
        foreach (Light light in _gameManager.mainLights)
        {
            if (light == null || !light.enabled || !light.gameObject.activeInHierarchy)
                continue;

            if (IsLitByLight(light))
                return true;
        }

        return false;
    }

    private bool IsLitByLight(Light light)
    {
        Vector3 lightPosition = light.transform.position;
        Vector3 lightDirection = light.transform.forward;

        Ray ray = new Ray(lightPosition, lightDirection);
        RaycastHit hit;

        if (Physics.SphereCast(ray, _sphereCastRadius, out hit, _lightDetectionRange))
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    public void DoRotate()
    {
        transform.Rotate(0, 30 * Time.deltaTime, 0);
    }

    #endregion   
}