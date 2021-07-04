using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private PlayerController playerPRefab;

    private PlayerController player;

    public PlayerController Player => player;

    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        player = Instantiate(playerPRefab);
    }
}
