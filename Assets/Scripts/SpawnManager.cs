using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyPoolManager enemyPool;
    [SerializeField] private PowerupPoolManager powerupPool;

    public void Restart()
    {
        enemyPool.Restart();
        powerupPool.Restart();
    }
}
