using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPoolManager : ObjectsSpawner<Enemy>
{
    [SerializeField] private Enemy bossPrefab;

    private float kamikadzeProbalility = 0.0f;

    private List<Enemy> enemies = new List<Enemy>();

    private bool isBossWave;
    private bool skipFirstSpawn;

    private Enemy boss;

    public int ActiveEnemiesCount => enemies.Count(e => e.gameObject.activeInHierarchy);

    protected override void ResetSpawner()
    {
        isBossWave = false;
        skipFirstSpawn = false;

        if (boss != null) {
            Destroy(boss);
        }
    }

    protected override void SpawnObject()
    {
        if (GameController.Instance.GameIsActive && !isBossWave) {
            if (ActiveEnemiesCount == 0 && skipFirstSpawn) {
                isBossWave = true;
                SpawnBoss();
            } else {
                Enemy enemy = GetObject();
                PreparePoolObject(enemy.gameObject);

                skipFirstSpawn = true;
            }
        }
    }

    protected override Enemy GetObject()
    {
        if (kamikadzeProbalility < 0.5f) {
            kamikadzeProbalility = ActiveEnemiesCount / 10.0f;
        }

        // TODO: need more flexible logic that will consider enemies spawn probabilities
        int index = Random.Range(0.0f, 1.0f) > kamikadzeProbalility ? 0 : 1;
        Enemy enemy = pools[index].GetPooledObject();
        if (!enemies.Contains(enemy)) {
            enemies.Add(enemy);
        }
        return enemy;
    }

    private void SpawnBoss()
    {
        boss = Instantiate(bossPrefab, GetRandomPosition(), bossPrefab.transform.rotation);
        boss.OnKill += GameController.Instance.GameWon;
    }
}
