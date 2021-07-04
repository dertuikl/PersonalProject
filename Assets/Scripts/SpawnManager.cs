using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Powerup powerupPrefab;

    private float xBound;
    private float zBound;

    private void Start()
    {
        // TODO
        xBound = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 0, 1)).x - 0.5f) / 2 - 2;
        zBound = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 0, 1)).y - 0.5f) / 2 - 2;

        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerups());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) {
            Instantiate(enemyPrefab, GetRandomPosition(), enemyPrefab.transform.rotation);

            yield return new WaitForSeconds(3.0f);
        }
    }

    private IEnumerator SpawnPowerups()
    {
        while (true)
        {
            Instantiate(powerupPrefab, GetRandomPosition(), powerupPrefab.transform.rotation);

            yield return new WaitForSeconds(5.0f);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float xPosition = Random.Range(-xBound, xBound);
        float zPosition = Random.Range(-zBound, zBound);
        return new Vector3(xPosition, 0.5f, zPosition);
    }
}
