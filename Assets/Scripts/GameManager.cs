using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Defenitions

    #region Variables

    [Header("Settings")]
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _playerTransform;
    public TextMeshProUGUI info;
    [SerializeField] private int _maxItems = 50;
    [SerializeField] private float _spawnRange = 25f;
    [SerializeField] private Light[] _playerLights;
    private bool _isGameStarted = false;
    private readonly List<GameObject> _allSpawnedItems = new();

    #endregion

    #region Properties

    public static GameManager Instance { get ; private set; }

    public IReadOnlyList<GameObject> AllSpawnedItems => _allSpawnedItems;

    public Transform PlayerTransform => _playerTransform;

    public Light[] PlayerLights => _playerLights;

    #endregion

    #endregion

    #region MonoBehaviourFunctions

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartGameWithDelay());
    }

    private void Update()
    {
        if (!_isGameStarted) 
            return;

        //float avgDistance = CalculateAverageDistance();
        //_infoDisplay.UpdateDisplay(ObjectSpawner.CollectedCount, _maxItems, avgDistance);

        //if (Input.GetKeyDown(KeyCode.Space))
        //    FindObjectOfType<ObjectSpawner>()?.ResetAll();
    }

    #endregion

    #region Functions

    private IEnumerator StartGameWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _isGameStarted = true;
        SpawnInitialItems();

        //gameObject.AddComponent<ScaleManager>();
        //gameObject.AddComponent<RotationManager>();
        //gameObject.AddComponent<ObjectSpawner>();
    }

    private void SpawnInitialItems()
    {
        for (int i = 0; i < _maxItems; i++)
        {
            GameObject item = Instantiate(_itemPrefab, RandomPosition(), Quaternion.identity);
            item.transform.localScale = Vector3.one * Random.Range(0.2f, 1.7f);
            item.name = $"Item_{i}";

            //var collectable = item.GetComponent<CollectableItem>();
            //if (collectable != null) 
            //    collectable.Initialize(this);

            //_allSpawnedItems.Add(item);
        }
    }

    private float CalculateAverageDistance()
    {
        if (_allSpawnedItems.Count == 0)
            return 0;

        float total = 0;
        int count = 0;

        foreach (var o in _allSpawnedItems)
        {
            if (o == null) continue;
            total += Vector3.Distance(_playerTransform.position, o.transform.position);
            count++;
        }

        return count == 0 ? 0 : total / count;
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-_spawnRange, _spawnRange), Random.Range(2, 5), Random.Range(-_spawnRange, _spawnRange));
    }

    #endregion
}