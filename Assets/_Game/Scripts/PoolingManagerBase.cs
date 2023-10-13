using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManagerBase<T, K> : MonoBehaviour where T : MonoBehaviour where K : MonoBehaviour{
    [SerializeField] private int amountToPool = 10;
    [SerializeField] protected K objectPool;
    [SerializeField] protected Transform content;

    protected List<K> pooledObjects;
    private RectTransform originRectTransformObject;
    private Transform originTransformObject;
    
    private void Start() {
        pooledObjects = new List<K>();
        GetSetupOrigin();
        for (int i = 0; i < amountToPool; i++) {
            CreateObject();
        }
        
        objectPool.gameObject.SetActive(false);
    }

    private void GetSetupOrigin() {
        if (objectPool.TryGetComponent(out RectTransform rect)) {
            originRectTransformObject = rect;
        }
        
        if (objectPool.TryGetComponent(out Transform trans)) {
            originTransformObject = trans;
        }
    }

    private K CreateObject() {
        var obj = Instantiate(objectPool, content);
        obj.gameObject.name = objectPool.name + pooledObjects.Count;
        obj.gameObject.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    protected K GetObjectPooledAvailable() {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].gameObject.activeInHierarchy) {
                return pooledObjects[i];
            }
        }

        return CreateObject();
    }

    protected void ResetSetupPoolObject(K obj) {
        if (originRectTransformObject) {
            var rect = obj.GetComponent<RectTransform>();
            rect.anchorMin = originRectTransformObject.anchorMin;
            rect.anchorMax = originRectTransformObject.anchorMax;
            rect.pivot = originRectTransformObject.pivot;
            rect.anchoredPosition = originRectTransformObject.anchoredPosition;
        }
    }
    
}
