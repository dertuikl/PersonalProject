using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public void OnShowStatsOnGameScreenToggle(bool value) => UserData.ShowShootingStatsToggle();

    public void OnClickMenu()
    {
        GameManager.Instance.OpenMenuScreen();
    }

    public void OnClickRestart()
    {
        GameController.Instance.RestartGame();
        OnClickBackToGame();
    }

    public void OnClickBackToGame()
    {
        GameController.Instance.GameIsActive = true;
        GameManager.Instance.OpenGameScreen();
    }
}
