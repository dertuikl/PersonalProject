using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvasStabilizer : MonoBehaviour
{
    private readonly Vector3 cameraFacingRotation = new Vector3(90, 0, 0);

    [SerializeField] private Transform targetTransform;
    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position;
    }

    private void LateUpdate()
    {
        transform.eulerAngles = cameraFacingRotation;
        transform.position = targetTransform.position + offset;
    }
}
