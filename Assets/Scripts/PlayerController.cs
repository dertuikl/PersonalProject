using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsShooting { get; private set; }

    public Action OnKill;

    [SerializeField] private PlayerTrigger playerTrigger;

    private Enemy target;
    private List<Enemy> enemiesInRange = new List<Enemy>();

    private int health => UserData.Health;

    private void Start()
    {
        playerTrigger.OnTriggerContentChanged += (updatedList) => enemiesInRange = updatedList;
    }

    private void Update()
    {
        UpdateTarget();

        if (target) {
            transform.LookAt(target.transform);
        }

        IsShooting = target;
    }

    private void UpdateTarget()
    {
        if(enemiesInRange.Count > 0) {
            enemiesInRange = enemiesInRange.OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).ToList();
            if (target && target != enemiesInRange[0]) {
                target.OnKill -= OnKillEnemy;
                enemiesInRange[0].OnKill += OnKillEnemy;
            }
            if(target == null) {
                enemiesInRange[0].OnKill += OnKillEnemy;
            }
            target = enemiesInRange[0];
        } else {
            target = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>()) {
            UpdateHealth(-1);
        } else if (other.GetComponent<Powerup>()) {
            other.GetComponent<Powerup>().Use(this);
        }
    }

    public void UpdateHealth(int points)
    {
        UserData.SetPlayerHealth(health + points);
        if(health <= 0) {
            OnKill();
            Destroy(gameObject);
        }
    }

    private void OnKillEnemy(Enemy enemy)
    {
        enemy.OnKill -= OnKillEnemy;
        UserData.SetPlayerScore(UserData.Score + enemy.KillScore);
    }

    public void UpdateAttackSpeed(float multiplier)
    {
        if(multiplier > 0) {
            UserData.SetAttackSpeed(UserData.AttackSpeed * multiplier);
        }
    }

    public void UpdateWeaponSpeed(float multiplier)
    {
        if (multiplier > 0) {
            UserData.SetWeaponSpeed(UserData.WeaponSpeed * multiplier);
        }
    }

    public void UpdateWeaponDamage(float multiplier)
    {
        if (multiplier > 0) {
            UserData.SetWeaponDamage(UserData.WeaponDamage * multiplier);
        }
    }
}
