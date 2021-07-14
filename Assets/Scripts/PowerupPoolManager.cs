using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPoolManager : ObjectsSpawner<Powerup>
{
    protected override void SpawnObject()
    {
        if (GameController.Instance.GameIsActive) {
            Powerup powerup = GetObject();
            PreparePoolObject(powerup.gameObject);
        }
    }

    protected override Powerup GetObject() => pools.RandomElement().GetPooledObject();
}
