using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Action<List<Enemy>> OnTriggerContentChanged;

    private List<Enemy> enemiesInRange = new List<Enemy>();

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy && !enemiesInRange.Contains(enemy)) {
            enemiesInRange.Add(enemy);
            enemy.OnKill += RemoveKilledEnemy;
            OnTriggerContentChanged?.Invoke(enemiesInRange);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy && enemiesInRange.Contains(enemy)) {
            enemiesInRange.Remove(enemy);
            OnTriggerContentChanged?.Invoke(enemiesInRange);
        }
    }

    private void RemoveKilledEnemy(Enemy enemy)
    {
        enemy.OnKill -= RemoveKilledEnemy;

        enemiesInRange.Remove(enemy);
        OnTriggerContentChanged?.Invoke(enemiesInRange);
    }
}
