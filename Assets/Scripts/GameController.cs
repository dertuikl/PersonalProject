using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool GameIsActive;
    public bool GameIsInProgress { get; private set; }
    public Vector2 ViewWorldBounds { get; private set; }

    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameScreen gameScreen;
    [SerializeField] private PlayerController playerPrefab;

    // ENCAPSULATION
    public PlayerController Player { get; private set; }
    public List<Enemy> EnemiesOnField => spawnManager.Enemies;

    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }

        CalculateViewWorldBounds();
    }

    public void RestartGame()
    {
        if (Player) {
            Destroy(Player.gameObject);
            Player = null;
        }
        spawnManager.Restart();
        UserData.ResetCurrentGameData();
        StartGame();
    }

    private void StartGame()
    {
        Player = Instantiate(playerPrefab);
        Player.OnKill += GameOver;
        GameIsActive = true;
        GameIsInProgress = true;
    }

    private void CalculateViewWorldBounds()
    {
        float xBound = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 0, 1)).x - 0.5f) / 2;
        float yBound = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 0, 1)).y - 0.5f) / 2;
        ViewWorldBounds = new Vector2(xBound, yBound);
    }

    public void GameOver()
    {
        Player.OnKill -= GameOver;
        GameIsActive = false;
        GameIsInProgress = false;

        gameScreen.SetGameOverState();
    }

    public void GameWon(Enemy enemy)
    {
        GameIsActive = false;
        GameIsInProgress = false;

        gameScreen.SetGameWonState();
    }
}
