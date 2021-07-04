using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsShooting { get; private set; }

    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private Bullet bulletPRefab;

    private Enemy target;

    private void Start()
    {
        StartCoroutine(ShootCor());
    }

    private void Update()
    {
        if(target == null) {
            // TODO: replace with less weight logic
            // TODO: move to spawn manager (?)
            List<Enemy> enemiesOnField = FindObjectsOfType<Enemy>().ToList();
            if(enemiesOnField.Count > 0) {
                enemiesOnField.OrderBy(e => Vector3.Distance(e.transform.position, transform.position));
                target = enemiesOnField.First();
            }
        }

        if (target) {
            transform.LookAt(target.transform);
        }

        IsShooting = target;
    }

    private IEnumerator ShootCor()
    {
        // TODO: while !gameOver or smth
        while (true) {
            if (IsShooting) {
                Shoot();
            }

            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPRefab, transform.position, transform.rotation); ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>()) {
            Debug.Log("Player hit enemy " + other.gameObject.name);
        } else if (other.GetComponent<Powerup>()) {
            Debug.Log("player hit powerup " + other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
}
