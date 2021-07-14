using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class ObjectsSpawner<T> : MonoBehaviour where T : MonoBehaviour
{ 
    [SerializeField] protected float startSpawnDelay;
    [SerializeField] protected float spawnDelay;

    [SerializeField] protected List<T> prefabs;

    protected List<Pool<T>> pools = new List<Pool<T>>();

    private float xBound;
    private float zBound;

    protected virtual void Awake()
    {
        xBound = GameController.Instance.ViewWorldBounds.x - 2;
        zBound = GameController.Instance.ViewWorldBounds.y - 2;

        foreach (T prefab in prefabs) {
            Pool<T> newPool = new Pool<T>(prefab, Instantiate);
            pools.Add(newPool);
        }
    }

    public void Restart()
    {
        StopAllCoroutines();
        ResetSpawner();
        StartCoroutine(SpawnObjects());
    }

    public virtual void ResetSpawner() { }

    protected virtual IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(startSpawnDelay);

        while (true) {
            SpawnObject();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    protected abstract void SpawnObject();

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
