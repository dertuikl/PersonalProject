using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private PlayerController player => GameController.Instance.Player;

    private void Update()
    {
        if (player) {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        Vector3 moveDirection = (player.transform.position - transform.position).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
