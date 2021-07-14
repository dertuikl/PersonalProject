using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPoolManager : ObjectsSpawner<Powerup>
{
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
