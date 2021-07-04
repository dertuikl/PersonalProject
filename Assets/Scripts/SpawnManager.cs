﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float enemiesSpawnDelay = 3.0f;
    [SerializeField] private float powerupsSpawnDelay = 5.0f;

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Powerup powerupPrefab;

    private float xBound;
    private float zBound;

    private Pool<Enemy> enemiesPool;

    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

    private void Start()
    {
        xBound = GameController.Instance.ViewWorldBounds.x - 2;
        zBound = GameController.Instance.ViewWorldBounds.y - 2;

        enemiesPool = new Pool<Enemy>(enemyPrefab, Instantiate);

        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerups());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) {
            if (GameController.Instance.GameIsActive) {
                Enemy enemy = enemiesPool.GetPooledObject();
                enemy.gameObject.SetActive(true);
                enemy.transform.position = GetRandomPosition();
                enemy.OnKill += OnKillEnemy;
                Enemies.Add(enemy);
            }

            yield return new WaitForSeconds(enemiesSpawnDelay);
        }
    }

    private IEnumerator SpawnPowerups()
    {
        while (true)
        {
            if (GameController.Instance.GameIsActive) {
                Instantiate(powerupPrefab, GetRandomPosition(), powerupPrefab.transform.rotation);
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
