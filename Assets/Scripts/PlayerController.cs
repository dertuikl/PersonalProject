using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsShooting { get; private set; }

    public Action OnKill;

    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private int maxHealth = 9;
    [SerializeField] private Bullet bulletPRefab;

    private Enemy target;
    private int currentHealth;
    private Pool<Bullet> bulletsPool;

    private void Start()
    {
        currentHealth = maxHealth;
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
            Debug.Log("player hit powerup " + other.gameObject.name);
            Destroy(other.gameObject);
        }
    }

    public void UpdateHealth(int points)
    {
        currentHealth += points;
        Debug.Log("player's health: " + currentHealth + "/" + maxHealth);
        if(currentHealth <= 0) {
            OnKill();
            Destroy(gameObject);
        }
    }
}
