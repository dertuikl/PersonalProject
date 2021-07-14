using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPoolManager : ObjectsSpawner<Powerup>
{
    [SerializeField] private List<Powerup> powerupPrefabs;

    protected override void Awake()
    {
        base.Awake();

        foreach (var powerupPrefab in powerupPrefabs) {
            Pool<Powerup> newPool = new Pool<Powerup>(powerupPrefab, Instantiate);
            pools.Add(newPool);
        }
    }

    public override void StartGame()
    {
        StartCoroutine(SpawnPowerups());
    }

    private IEnumerator SpawnPowerups()
    {
        yield return new WaitForSeconds(startSpawnDelay);

        while (true) {
            if (GameController.Instance.GameIsActive) {
                Powerup powerup = GetObject();
                PreparePoolObject(powerup.gameObject);
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    protected override Powerup GetObject() => pools.RandomElement().GetPooledObject();
}
