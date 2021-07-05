using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private List<T> pool = new List<T>();
    private T objectPrefab;
    private Func<T, T> instantiateFunc;

    public Pool(T objectPrefab, Func<T, T> instantiateFunc)
    {
        this.objectPrefab = objectPrefab;
        this.instantiateFunc = instantiateFunc;
    }

    public T GetPooledObject()
    {
        T obj = pool.FirstOrDefault(o => !o.gameObject.activeInHierarchy);
        if (!obj) {
            obj = instantiateFunc(objectPrefab);
            pool.Add(obj);
        }

        return obj;
    }

    public void DisableAllObjects() => pool.ForEach(o => o.gameObject.SetActive(false));
}
