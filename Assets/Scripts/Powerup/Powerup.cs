using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    [SerializeField] protected float impactValue = 1f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private float spawnAnimationTime = 0.3f;

    protected abstract void SpecificInfluence(PlayerController player);

    public void Use(PlayerController player)
    {
        SpecificInfluence(player);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(SpawnAnimation());
    }

    private IEnumerator SpawnAnimation()
    {
        float startTime = Time.time;
        Vector3 targetScale = transform.localScale;
        while(Time.time - startTime < spawnAnimationTime) {
            Vector3 currentScale = Vector3.Lerp(Vector3.zero, targetScale, (Time.time - startTime) / spawnAnimationTime);
            transform.localScale = currentScale;
            yield return null;
        }
    }

    private void Update()
    {
        if (GameController.Instance.GameIsActive) {
            transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
        }
    }
}
