using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Action<Enemy> OnKill;

    [SerializeField] private bool useAgentToMove = true;

    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int killScore = 1;

    [SerializeField] protected NavMeshAgent agent;

    [SerializeField] private Transform body;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private new BoxCollider collider;

    private float currentHealth;
    private float xBound;
    private float zBound;

    public int KillScore => killScore;
    protected PlayerController player;// => GameController.Instance.Player;
    private float objectWidth => collider.size.z * 2;

    private void Start()
    {
        xBound = GameController.Instance.ViewWorldBounds.x - objectWidth;
        zBound = GameController.Instance.ViewWorldBounds.y - objectWidth;
    }

    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;

        player = GameController.Instance.Player;

        AgentSetup();
        UpdateHealthSlider();
    }

    private void AgentSetup()
    {
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        if (player && GameController.Instance.GameIsActive) {
            MoveToPlayer();
        } else if(useAgentToMove) {
            agent.isStopped = true;
        }

        ConstrainPosition();
    }

    protected virtual void MoveToPlayer()
    {
        if (useAgentToMove) {
            agent.SetDestination(player.transform.position);
            agent.isStopped = false;
        } else {
            Vector3 moveVector = player.transform.position - transform.position;
            // TODO: how track bullet hit in that case?
            //collider.enabled = moveVector.magnitude < collider.size.x;

            Vector3 moveDirection = moveVector.normalized;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            body.LookAt(player.transform);
        }
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

    private void ConstrainPosition()
    {
        float xPosCalculated = Mathf.Clamp(transform.position.x, -xBound, xBound);
        float zPosCalculated = Mathf.Clamp(transform.position.z, -zBound, zBound);
        transform.position = new Vector3(xPosCalculated, transform.position.y, zPosCalculated);
    }
}
