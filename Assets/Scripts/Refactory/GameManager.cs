using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Definitions

    #region Variables

    [Header("References")]
    [SerializeField] private GameObject _collectibleItemPrefab;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Light[] _mainLights;

    [Header("Settings")]
    [SerializeField] private int _maxCollectibleItems = 50;
    [SerializeField] private float _spawnRange = 25f;

    private ObjectSpawner _objectSpawner;
    private ItemPool _itemPool;
    private bool _isGameStarted = false;

    #endregion

    #region Properties

    public static GameManager Instance { get; private set; }
    public static List<GameObject> AllCollectibleItems { get; private set; } = new();
    public Light[] mainLights => _mainLights;

    #endregion

    #endregion

    #region MonoBehaviourFunctions

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitializeSystems();
    }

    private void Start()
    {
        StartCoroutine(InitializeGame());
    }

    private void Update()
    {
        if (!_isGameStarted) 
            return;

        UpdateUI();
        HandleInput();
    }

    #endregion

    #region Functions

    private void InitializeSystems()
    {
        _itemPool = gameObject.AddComponent<ItemPool>();
        _itemPool.Initialize(_collectibleItemPrefab, _maxCollectibleItems * 2);

        _objectSpawner = gameObject.AddComponent<ObjectSpawner>();
        gameObject.AddComponent<GameplaySystem>();
        gameObject.AddComponent<AnalyticsManager>();
    }

    private IEnumerator InitializeGame()
    {
        yield return new WaitForSeconds(0.5f);

        _objectSpawner.SpawnInitialItems(_maxCollectibleItems, _spawnRange);
        _isGameStarted = true;
    }

    private void UpdateUI()
    {
        float averageDistance = CalculateAverageDistance();
        _infoText.text = $"Collected: {ObjectSpawner.CollectedCount}/{_maxCollectibleItems} Avg: {averageDistance:F1}";
    }

    private float CalculateAverageDistance()
    {
        if (AllCollectibleItems.Count == 0) 
            return 0f;

        float totalDistance = 0f;
        int validItems = 0;

        foreach (var item in AllCollectibleItems)
        {
            if (item != null && item.activeInHierarchy)
            {
                totalDistance += Vector3.Distance(_playerTransform.position, item.transform.position);
                validItems++;
            }
        }

        return validItems > 0 ? totalDistance / validItems : 0f;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _objectSpawner.ResetAll();
        }
    }

    public GameObject GetItemFromPool() => _itemPool.GetItem();

    public void ReturnItemToPool(GameObject item) => _itemPool.ReturnItem(item);

    #endregion
}
