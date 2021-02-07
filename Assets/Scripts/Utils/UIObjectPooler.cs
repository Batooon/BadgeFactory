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
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject pooledObjectInstance = Instantiate(_objectToPool);
            pooledObjectInstance.transform.SetParent(_attachedCanvas.transform);
            pooledObjectInstance.transform.position = transform.position;
            pooledObjectInstance.transform.localScale = Vector3.one;
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
