using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Bullet bulletPRefab;

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
