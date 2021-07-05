using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float enemiesSpawnDelay = 3.0f;
    [SerializeField] private float powerupsSpawnDelay = 5.0f;
    [SerializeField] private float startPowerupsSpawnDelay = 2.0f;

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Enemy bossPrefab;
    [SerializeField] private List<Powerup> powerupPrefabs;

    private float xBound;
    private float zBound;

    private Pool<Enemy> enemiesPool;

    private bool isBossWave;
    private bool skipFirstSpawn;

    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

    private void Start()
    {
        xBound = GameController.Instance.ViewWorldBounds.x - 2;
        zBound = GameController.Instance.ViewWorldBounds.y - 2;

        enemiesPool = new Pool<Enemy>(enemyPrefab, Instantiate);

        Restart();
    }

    public void Restart()
    {
        isBossWave = false;
        skipFirstSpawn = false;
        Enemies.ForEach(e => e.gameObject.SetActive(false));
        Enemies.Clear();

        StopAllCoroutines();

        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerups());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) {
            if (GameController.Instance.GameIsActive && !isBossWave) {
                if (Enemies.Count == 0 && skipFirstSpawn) {
                    isBossWave = true;
                    SpawnBoss();
                } else {
                    Enemy enemy = enemiesPool.GetPooledObject();
                    enemy.gameObject.SetActive(true);
                    enemy.transform.position = GetRandomPosition();
                    enemy.OnKill += OnKillEnemy;
                    Enemies.Add(enemy);

                    skipFirstSpawn = true;
                }
            }

            yield return new WaitForSeconds(enemiesSpawnDelay);
        }
    }

    private void SpawnBoss()
    {
        Enemy boss = Instantiate(bossPrefab, GetRandomPosition(), bossPrefab.transform.rotation);
        boss.OnKill += GameController.Instance.GameWon;
        Enemies.Add(boss);
    }

    private IEnumerator SpawnPowerups()
    {
        yield return new WaitForSeconds(startPowerupsSpawnDelay);

        while (true) {
            int index = Random.Range(0, powerupPrefabs.Count);

            if (GameController.Instance.GameIsActive) {
                Instantiate(powerupPrefabs[index], GetRandomPosition(), powerupPrefabs[index].transform.rotation);
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

    private void OnKillEnemy(Enemy enemy)
    {
        enemy.OnKill -= OnKillEnemy;
        Enemies.Remove(enemy);
    }
}
