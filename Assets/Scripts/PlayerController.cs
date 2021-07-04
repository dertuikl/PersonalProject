﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsShooting { get; private set; }

    public Action OnKill;

    [SerializeField] private float startShootDelay = 0.5f;
    [SerializeField] private float startBulletSpeed = 20f;
    [SerializeField] private float startBulletDamage = 18f;
    [SerializeField] private int maxHealth = 9;
    [SerializeField] private Bullet bulletPRefab;

    private Enemy target;

    private float currentShootDelay = 0.5f;
    private float currentBulletSpeed = 20f;
    private float currentBulletDamage = 18f;
    private int currentHealth;
    private int currentScore;
    private Pool<Bullet> bulletsPool;

    private void Start()
    {
        currentHealth = maxHealth;
        currentScore = 0;
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
            target = enemiesOnField[0];
        }
    }

    private IEnumerator ShootCor()
    {
        while (true) {
            if (IsShooting && GameController.Instance.GameIsActive) {
                Shoot();
            }

            yield return new WaitForSeconds(startShootDelay);
        }
    }

    private void Shoot()
    {
        Bullet newBullet = bulletsPool.GetPooledObject();
        newBullet.gameObject.SetActive(true);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = transform.rotation;
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
        currentHealth = Mathf.Clamp(currentHealth + points, 0, maxHealth);
        Debug.Log("player's health: " + currentHealth + "/" + maxHealth);
        if(currentHealth <= 0) {
            OnKill();
            Destroy(gameObject);
        }
    }

    private void OnKillEnemy(Enemy enemy)
    {
        enemy.OnKill -= OnKillEnemy;
        currentScore += enemy.KillScore;
        Debug.Log("player's score: " + currentScore);
    }
}
