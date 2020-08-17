using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pooledObjects;
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _amountToPool;

    public void Init()
    {
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject pooledObjectInstance = Instantiate(_objectToPool);
            pooledObjectInstance.SetActive(false);
            _pooledObjects.Add(pooledObjectInstance);
        }
    }

    public GameObject GetPooledObjects()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (_pooledObjects[i].activeInHierarchy == false)
                return _pooledObjects[i];
        }
        return null;
    }
}
