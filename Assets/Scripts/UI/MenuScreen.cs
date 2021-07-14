using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private List<RectTransform> tutorialPositions = new List<RectTransform>();
    [SerializeField] private List<GameObject> tutorialObjects = new List<GameObject>();

    private void OnEnable()
    {
        bool gameIsInProgress = GameController.Instance.GameIsInProgress;
        continueButton.gameObject.SetActive(gameIsInProgress);
        RefreshTutorialView();
    }

    public void OnCLickNewGameButton()
    {
        GameController.Instance.RestartGame();
        OnClickContinueButton();
    }

    public void OnClickContinueButton()
    {
        GameController.Instance.GameIsActive = true;
        ScreenManager.Instance.OpenGameScreen();
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void RefreshTutorialView()
    {
        List<Vector3> positions = new List<Vector3>();

        foreach(RectTransform rt in tutorialPositions) {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, rt.position, Camera.main, out Vector3 worldPosition);
            positions.Add(worldPosition);
        }

        for(int i = 0; i < tutorialObjects.Count; i++) {
            tutorialObjects[i].transform.position = positions[i];
        }
    }
}
