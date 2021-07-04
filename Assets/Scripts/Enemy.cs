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

    [SerializeField] private Transform body;
    [SerializeField] private Slider healthSlider;

    private float currentHealth;

    private PlayerController player => GameController.Instance.Player;

    private void Start()
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
        Vector3 moveDirection = (player.transform.position - transform.position).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        body.LookAt(player.transform);
    }

    public void Hit(float points)
    {
        if(points > 0) {
            currentHealth -= points;
            UpdateHealthSlider();
        }

        if(currentHealth <= 0) {
            OnKill?.Invoke(this);
            Destroy(gameObject);
        }
    }

    private void UpdateHealthSlider()
    {
        healthSlider.value = currentHealth;
        healthSlider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, currentHealth / maxHealth);
    }
}
