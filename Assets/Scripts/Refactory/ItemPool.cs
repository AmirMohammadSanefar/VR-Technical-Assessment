using System.Collections.Generic;
using UnityEngine;

namespace Refactory
{
    public class ItemPool : MonoBehaviour
    {
        #region Definitions

        #region Variables

        private Queue<GameObject> _pool = new();
        private GameObject _itemPrefab;
        private int _poolSize;

        #endregion

        #endregion

        #region Functions

        public void Initialize(GameObject prefab, int size)
        {
            _itemPrefab = prefab;
            _poolSize = size;
            GrowPool();
        }

        private void GrowPool()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject newItem = Instantiate(_itemPrefab);
                newItem.SetActive(false);
                _pool.Enqueue(newItem);
            }
        }

        public GameObject GetItem()
        {
            if (_pool.Count == 0)
            {
                GrowPool();
            }

            GameObject item = _pool.Dequeue();
            return item;
        }

        public void ReturnItem(GameObject item)
        {
            item.SetActive(false);
            _pool.Enqueue(item);
        }

        #endregion
    }
}