using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameFinishedView : MonoBehaviour
{
    public enum GameFinishState {
        None,
        Win,
        Lose
    }

    private GameFinishState state;

    public GameFinishState State {
        get => state;
        set {
            state = value;
            UpdateView();
        }
    }

    [SerializeField] private Color winColor;
    [SerializeField] private Color loseColor;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI gameFinishedText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void UpdateView()
    {
        gameObject.SetActive(state != GameFinishState.None);
        if(state != GameFinishState.None) {
            backgroundImage.color = state == GameFinishState.Win ? winColor : loseColor;
            gameFinishedText.text = state == GameFinishState.Win ? "YOU WON!" : "GAME OVER";
            scoreText.text = $"Score: {UserData.Score}";
        }
    }

    public void OnClickRestart()
    {
        GameController.Instance.RestartGame();
        GameController.Instance.GameIsActive = true;
        State = GameFinishState.None;
    }
}
