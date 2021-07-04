using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
    }
}
