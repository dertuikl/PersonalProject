using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    [SerializeField] protected float impactValue = 1f;
    [SerializeField] private float rotateSpeed = 100f;

    protected abstract void SpecificInfluence(PlayerController player);

    public void Use(PlayerController player)
    {
        SpecificInfluence(player);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
    }
}
