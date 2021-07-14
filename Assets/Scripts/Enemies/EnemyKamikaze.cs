using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikaze : Enemy
{
    [SerializeField] private float chargeTime = 3f;
    [SerializeField] private float attackTime = 1f;

    private bool isCharged;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(ChargeCycle());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    protected override void MoveToPlayer()
    {
        agent.isStopped = !isCharged;
    }

    private IEnumerator ChargeCycle()
    {
        while (true) {
            yield return new WaitForSeconds(chargeTime);

            if (GameController.Instance.GameIsActive) {
                isCharged = true;
                agent.SetDestination(player.transform.position);
            }

            yield return new WaitForSeconds(attackTime);

            isCharged = false;
        }
    }
}
