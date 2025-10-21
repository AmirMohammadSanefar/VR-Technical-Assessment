using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    #region Definitions

    #region Variables

    private List<GameObject> _spawnedObjects = new();
    private GameManager _gameManager;
    private Coroutine _spawnCoroutine;

    #endregion

    #region Properties

    public static int CollectedCount { get; private set; } = 0;

    #endregion

    #endregion

    #region MonoBehaviourFunctions

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    #endregion

    #region Functions

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            if (_gameManager != null && GameManager.AllCollectibleItems.Count < 100)
            {
                SpawnItem();
            }
        }
    }

    public void SpawnInitialItems(int count, float range)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnItemAtRandomPosition(range);
        }
    }

    private void SpawnItem()
    {
        var item = _gameManager.GetItemFromPool();
        if (item == null) return;

        SetupItem(item);
        _spawnedObjects.Add(item);
        GameManager.AllCollectibleItems.Add(item);
    }

    private void SpawnItemAtRandomPosition(float range)
    {
        var item = _gameManager.GetItemFromPool();
        if (item == null) return;

        Vector3 randomPosition = new(
            Random.Range(-range, range),
            Random.Range(2, 5),
            Random.Range(-range, range)
        );

        item.transform.position = randomPosition;
        item.transform.localScale = Vector3.one * Random.Range(0.2f, 1.7f);
        item.name = $"collectible_{GameManager.AllCollectibleItems.Count}";

        SetupItem(item);
        _spawnedObjects.Add(item);
        GameManager.AllCollectibleItems.Add(item);
    }

    private void SetupItem(GameObject item)
    {
        Vector3 spawnPosition = new(
            Random.Range(-10, 10),
            Random.Range(2, 7),
            Random.Range(-10, 10)
        );

        item.transform.position = spawnPosition;
        item.SetActive(true);

        var collectible = item.GetComponent<CollectibleItem>();
        if (collectible != null)
        {
            collectible.Initialize(_gameManager);
        }
    }

    public void ResetAll()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }

        foreach (var obj in _spawnedObjects)
        {
            if (obj != null)
            {
                _gameManager.ReturnItemToPool(obj);
            }
        }

        _spawnedObjects.Clear();
        GameUtility.ClearAll();
        CollectedCount = 0;

        StartCoroutine(RespawnItems());
    }

    private IEnumerator RespawnItems()
    {
        yield return new WaitForSeconds(0.1f);
        _spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    public static void IncrementCollectedCount() => CollectedCount++;
    public static void DecrementCollectedCount(int amount = 2) => CollectedCount -= amount;

    #endregion
}
