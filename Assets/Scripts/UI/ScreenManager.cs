using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject pauseScreen;

    private GameObject currentScreen;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    private void Start()
    {
        ChangeScreen(menuScreen);
    }

    public void OpenGameScreen()
    {
        ChangeScreen(gameScreen);
    }

    public void OpenPauseScreen()
    {
        ChangeScreen(pauseScreen);
    }

    public void OpenMenuScreen()
    {
        ChangeScreen(menuScreen);
    }

    //TODO: not in current submission
    public void ShowTutorialScreen()
    {
        Debug.Log("Show tutorial screen");
    }

    private void ChangeScreen(GameObject screen)
    {
        if (screen == currentScreen) {
            Debug.LogError("Attempt to open screen that already opened.");
            return;
        }

        if (currentScreen) {
            currentScreen.SetActive(false);
        }

        screen.SetActive(true);
        currentScreen = screen;
    }
}
