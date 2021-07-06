using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private GameFinishedView gameFinishedView;
    [SerializeField] private List<StatView> statsViews = new List<StatView>();

    private void OnEnable()
    {
        Refresh();

        UserData.OnUserDataChanged += Refresh;
    }

    private void OnDisable()
    {
        UserData.OnUserDataChanged -= Refresh;
    }

    public void Refresh()
    {
        statsViews.ForEach(v => v.Refresh());
    }

    public void OnClickPauseButton()
    {
        GameController.Instance.GameIsActive = false;
        GameManager.Instance.OpenPauseScreen();
    }

    public void SetGameOverState()
    {
        gameFinishedView.State = GameFinishedView.GameFinishState.Lose;
    }

    public void SetGameWonState()
    {
        gameFinishedView.State = GameFinishedView.GameFinishState.Win;
    }
}

public abstract class StatView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;

    protected abstract string Name();
    protected abstract float Value();

    public virtual void Refresh()
    {
        text.text = $"{Name()}: {Value()}";
    }
}