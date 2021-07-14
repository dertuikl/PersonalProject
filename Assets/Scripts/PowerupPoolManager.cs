using System.Collections.Generic;
using UnityEngine;

public class PowerupPoolManager : MonoBehaviour
{
    [SerializeField] private List<Powerup> powerupPrefabs;

    private List<Pool<Powerup>> pools = new List<Pool<Powerup>>();

    private void Awake()
    {
        foreach (var powerupPrefab in powerupPrefabs) {
            Pool<Powerup> newPool = new Pool<Powerup>(powerupPrefab, Instantiate);
            pools.Add(newPool);
        }
    }

    public Powerup GetObject() => pools.RandomElement().GetPooledObject();

    public void DisableAllObjects() => pools.ForEach(p => p.DisableAllObjects());
}
