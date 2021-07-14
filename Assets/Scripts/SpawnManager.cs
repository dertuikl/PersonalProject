using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float enemiesSpawnDelay = 3.0f;
    [SerializeField] private float powerupsSpawnDelay = 5.0f;
    [SerializeField] private float startPowerupsSpawnDelay = 2.0f;

    [SerializeField] private Enemy bossPrefab;

    [SerializeField] private EnemyPoolManager enemyPool;
    [SerializeField] private PowerupPoolManager powerupPool;

    private float xBound;
    private float zBound;
    private bool isBossWave;
    private bool skipFirstSpawn;

    private Enemy boss;

    private void Start()
    {
        xBound = GameController.Instance.ViewWorldBounds.x - 2;
        zBound = GameController.Instance.ViewWorldBounds.y - 2;
    }

    public void Restart()
    {
        isBossWave = false;
        skipFirstSpawn = false;
        enemyPool.DisableAllObjects();
        if (boss != null) {
            Destroy(boss);
        }
        powerupPool.DisableAllObjects();

        StopAllCoroutines();

        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerups());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) {
            if (GameController.Instance.GameIsActive && !isBossWave) {
                if (enemyPool.ActiveEnemiesCount == 0 && skipFirstSpawn) {
                    isBossWave = true;
                    SpawnBoss();
                } else {
                    Enemy enemy = enemyPool.GetObject();
                    PreparePoolObject(enemy.gameObject);

                    skipFirstSpawn = true;
                }
            }

            yield return new WaitForSeconds(enemiesSpawnDelay);
        }
    }

    private void SpawnBoss()
    {
        boss = Instantiate(bossPrefab, GetRandomPosition(), bossPrefab.transform.rotation);
        boss.OnKill += GameController.Instance.GameWon;
    }

    private IEnumerator SpawnPowerups()
    {
        yield return new WaitForSeconds(startPowerupsSpawnDelay);

        while (true) {
            if (GameController.Instance.GameIsActive) {
                Powerup powerup = powerupPool.GetObject();
                PreparePoolObject(powerup.gameObject);
            }

            yield return new WaitForSeconds(powerupsSpawnDelay);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float xPosition = Random.Range(-xBound, xBound);
        float zPosition = Random.Range(-zBound, zBound);
        return new Vector3(xPosition, 0.5f, zPosition);
    }

    private void PreparePoolObject(GameObject poolObject)
    {
        poolObject.SetActive(true);
        poolObject.transform.position = GetRandomPosition();
    }
}
