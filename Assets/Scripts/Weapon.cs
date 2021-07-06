using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Bullet bulletPRefab;

    private Pool<Bullet> bulletsPool;

    private float attackSpeed => UserData.AttackSpeed;
    private float weaponSpeed => UserData.WeaponSpeed;
    private float weaponDamage => UserData.WeaponDamage;
    private float shootDelay => 10 / attackSpeed;

    private void Start()
    {
        bulletsPool = new Pool<Bullet>(bulletPRefab, Instantiate);
        StartCoroutine(ShootCor());
    }

    private IEnumerator ShootCor()
    {
        while (true) {
            if (player.IsShooting && GameController.Instance.GameIsActive) {
                Shoot();
            }

            yield return new WaitForSeconds(shootDelay);
        }
    }

    // ABSTRACTION
    private void Shoot()
    {
        Bullet newBullet = bulletsPool.GetPooledObject();
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = transform.rotation;
        newBullet.SetStats(weaponSpeed, weaponDamage);
        newBullet.gameObject.SetActive(true);
    }
}
