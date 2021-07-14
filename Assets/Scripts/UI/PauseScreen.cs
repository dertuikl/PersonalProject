using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Sprite checkToggle;
    [SerializeField] private Sprite uncheckToggle;
    [SerializeField] private Image toggleImage;

    public void OnShowStatsOnGameScreenToggle(bool value)
    {
        UserData.ShowShootingStatsToggle();
        toggleImage.sprite = UserData.ShowShootingStats ? checkToggle : uncheckToggle;
    }

    public void OnClickMenu()
    {
        ScreenManager.Instance.OpenMenuScreen();
    }

    public void OnClickRestart()
    {
        GameController.Instance.RestartGame();
        OnClickBackToGame();
    }

    public void OnClickBackToGame()
    {
        GameController.Instance.GameIsActive = true;
        ScreenManager.Instance.OpenGameScreen();
    }
}
