using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class ObjectsSpawner<T> : MonoBehaviour where T : MonoBehaviour
{ 
    [SerializeField] protected float startSpawnDelay;
    [SerializeField] protected float spawnDelay;

    protected List<Pool<T>> pools = new List<Pool<T>>();

    private float xBound;
    private float zBound;

    protected virtual void Awake()
    {
        xBound = GameController.Instance.ViewWorldBounds.x - 2;
        zBound = GameController.Instance.ViewWorldBounds.y - 2;
    }

    public void Restart()
    {
        StopAllCoroutines();
        StartGame();
    }

    public abstract void StartGame();

    protected void PreparePoolObject(GameObject poolObject)
    {
        poolObject.SetActive(true);
        poolObject.transform.position = GetRandomPosition();
    }

    protected Vector3 GetRandomPosition()
    {
        float xPosition = Random.Range(-xBound, xBound);
        float zPosition = Random.Range(-zBound, zBound);
        return new Vector3(xPosition, 0.5f, zPosition);
    }

    protected abstract T GetObject();

    public void DisableAllObjects() => pools.ForEach(p => p.DisableAllObjects());
}
