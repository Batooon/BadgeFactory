using System.Collections.Generic;
using UnityEngine;

public class UIObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private Canvas _attachedCanvas;
    [SerializeField] private int _amountToPool;
    private List<GameObject> _pooledObjects;

    public void Init()
    {
        Debug.Log($"Initializing Object Pooler of {_objectToPool}");
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject pooledObjectInstance = Instantiate(_objectToPool);
            pooledObjectInstance.transform.SetParent(_attachedCanvas.transform);
            pooledObjectInstance.transform.localPosition = Vector2.zero;
            pooledObjectInstance.transform.localScale = Vector3.one;
            pooledObjectInstance.SetActive(false);
            _pooledObjects.Add(pooledObjectInstance);
        }
        Debug.Log($"Pooled Objects Count {_pooledObjects.Count}");
    }

    public GameObject GetPooledObjects()
    {
        Debug.Log("Searching for object to pool");
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (_pooledObjects[i].activeInHierarchy == false)
            {
                Debug.Log("Found one!");
                return _pooledObjects[i];
            }
        }
        Debug.Log("Couldn't find any object to pool");
        return null;
    }
}
