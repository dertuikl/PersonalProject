using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsShooting { get; private set; }

    public Action OnKill;

    [SerializeField] private Bullet bulletPRefab;

    private Enemy target;
    private Pool<Bullet> bulletsPool;

    private int health => UserData.Health;
    private float attackSpeed => UserData.AttackSpeed;
    private float weaponSpeed => UserData.WeaponSpeed;
    private float weaponDamage => UserData.WeaponDamage;
    private float shootDelay => 10 / attackSpeed;

    private void Start()
    {
        bulletsPool = new Pool<Bullet>(bulletPRefab, Instantiate);

        StartCoroutine(ShootCor());
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
        List<Enemy> enemiesOnField = GameController.Instance.EnemiesOnField;
        if(enemiesOnField.Count > 0) {
            enemiesOnField = enemiesOnField.OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).ToList();
            if (target && target != enemiesOnField[0]) {
                target.OnKill -= OnKillEnemy;
                enemiesOnField[0].OnKill += OnKillEnemy;
            }
            if(target == null) {
                enemiesOnField[0].OnKill += OnKillEnemy;
            }
            target = enemiesOnField[0];
        }
    }

    private IEnumerator ShootCor()
    {
        while (true) {
            if (IsShooting && GameController.Instance.GameIsActive) {
                Shoot();
            }

            yield return new WaitForSeconds(shootDelay);
        }
    }

    // ABSTRACTION
    private void Shoot()
    {
        Bullet newBullet = bulletsPool.GetPooledObject();
        newBullet.gameObject.SetActive(true);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = transform.rotation;
        newBullet.SetStats(weaponSpeed, weaponDamage);
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
        //Debug.Log(UserData.Score + " " + enemy.KillScore);
        UserData.SetPlayerScore(UserData.Score + enemy.KillScore);
    }

    public void UpdateAttackSpeed(float multiplier)
    {
        if(multiplier > 0) {
            UserData.SetAttackSpeed(attackSpeed * multiplier);
        }
    }

    public void UpdateWeaponSpeed(float multiplier)
    {
        if (multiplier > 0) {
            UserData.SetWeaponSpeed(weaponSpeed * multiplier);
        }
    }

    public void UpdateWeaponDamage(float multiplier)
    {
        if (multiplier > 0) {
            UserData.SetWeaponDamage(weaponDamage * multiplier);
        }
    }
}
