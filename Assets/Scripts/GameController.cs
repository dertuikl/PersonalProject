using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Vector2 ViewWorldBounds { get; private set; }

    [SerializeField] private PlayerController playerPRefab;

    private PlayerController player;

    public PlayerController Player => player;

    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }

        CalculateViewWorldBounds();
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        player = Instantiate(playerPRefab);
    }

    private void CalculateViewWorldBounds()
    {
        float xBound = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 0, 1)).x - 0.5f) / 2;
        float yBound = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 0, 1)).y - 0.5f) / 2;
        ViewWorldBounds = new Vector2(xBound, yBound);
    }
}
