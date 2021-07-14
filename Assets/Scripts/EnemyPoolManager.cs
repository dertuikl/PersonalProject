using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemiesPrefabs;

    private float kamikadzeProbalility = 0.0f;

    private List<Pool<Enemy>> pools = new List<Pool<Enemy>>();
    private List<Enemy> enemies = new List<Enemy>();

    public int ActiveEnemiesCount => enemies.Count(e => e.gameObject.activeInHierarchy);

    private void Awake()
    {
        foreach (var enemyPrefab in enemiesPrefabs) {
            Pool<Enemy> newPool = new Pool<Enemy>(enemyPrefab, Instantiate);
            pools.Add(newPool);
        }
    }

    public Enemy GetObject()
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

    public void DisableAllObjects() => pools.ForEach(p => p.DisableAllObjects());
}
