using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Action<Enemy> OnKill;

    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int killScore = 1;

    [SerializeField] private Transform body;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private new BoxCollider collider;

    private float currentHealth;

    public int KillScore => killScore;
    private PlayerController player => GameController.Instance.Player;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        UpdateHealthSlider();
    }

    private void Update()
    {
        if (player && GameController.Instance.GameIsActive) {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        Vector3 moveVector = player.transform.position - transform.position;
        // TODO: how track bullet hit in that case?
        //collider.enabled = moveVector.magnitude < collider.size.x;

        Vector3 moveDirection = moveVector.normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        body.LookAt(player.transform);
    }

    public void Hit(float points)
    {
        if (points > 0) {
            currentHealth -= points;
            UpdateHealthSlider();
        }

        if(currentHealth <= 0) {
            gameObject.SetActive(false);
            OnKill?.Invoke(this);
        }
    }

    private void UpdateHealthSlider()
    {
        healthSlider.value = currentHealth;
        healthSlider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, currentHealth / maxHealth);
    }
}
