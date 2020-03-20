using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChanger : MonoBehaviour
{
    [SerializeField]
    private  GameObject MainMenuCanvas;
    [SerializeField]
    private  GameObject SnakeSelectionCanvas;
    [SerializeField]
    private  GameObject PlanetSelectionCanvas;
    [SerializeField]
    private  GameObject ObjectsRequiredForGamePlay;
    [SerializeField]
    private GameObject GameOverCanvas;
    [SerializeField]
    private GameObject gamePlayCanvas;
    [SerializeField]
    private GameObject tutorialCanvas;
    [SerializeField]
    private GameObject AddCoinCanvas;
    [SerializeField]
    private GameObject settingsCanvas;
    void Start()
    {
        Time.timeScale = 0f;
        if (MainMenuCanvas == null || SnakeSelectionCanvas == null || PlanetSelectionCanvas == null || GameOverCanvas == null)
            Debug.LogError("Some fields are unassigned");
        if (!restartChecker())
        {
            goToMainMenu();
        }
        else
        {
            startGame();
            GameStateHolder.restartClicked = "false";
        }

    }

    private bool restartChecker()
    {
        if (GameStateHolder.restartClicked.ToString() == "true") return true;
        return false;
    }

    public void goToSettingMenu()
    {
        MainMenuCanvas.SetActive(false);
        PlanetSelectionCanvas.SetActive(false);
        SnakeSelectionCanvas.SetActive(false);
        ObjectsRequiredForGamePlay.SetActive(false);
        GameOverCanvas.SetActive(false);
        gamePlayCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
        AddCoinCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void changeToSnakeMenu()
    {
        MainMenuCanvas.SetActive(false);
        PlanetSelectionCanvas.SetActive(false);
        SnakeSelectionCanvas.SetActive(true);
        ObjectsRequiredForGamePlay.SetActive(false);
        GameOverCanvas.SetActive(false);
        gamePlayCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
        AddCoinCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
    }

    public void goToAddCoinCanvas()
    {
        MainMenuCanvas.SetActive(false);
        PlanetSelectionCanvas.SetActive(false);
        SnakeSelectionCanvas.SetActive(false);
        ObjectsRequiredForGamePlay.SetActive(true);
        GameOverCanvas.SetActive(false);
        gamePlayCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
        AddCoinCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }

    public  void changeToPlanetMenu()
    {
        MainMenuCanvas.SetActive(false);
        PlanetSelectionCanvas.SetActive(true);
        SnakeSelectionCanvas.SetActive(false);
        ObjectsRequiredForGamePlay.SetActive(false);
        GameOverCanvas.SetActive(false);
        gamePlayCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
        AddCoinCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
    }

    public  void goToMainMenu()
    {
        MainMenuCanvas.SetActive(true);
        PlanetSelectionCanvas.SetActive(false);
        SnakeSelectionCanvas.SetActive(false);
        ObjectsRequiredForGamePlay.SetActive(true);
        GameOverCanvas.SetActive(false);
        gamePlayCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
        AddCoinCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
    }


    public  void startGame()
    {
        MainMenuCanvas.SetActive(false);
        PlanetSelectionCanvas.SetActive(false);
        SnakeSelectionCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
        gamePlayCanvas.SetActive(true);
        ObjectsRequiredForGamePlay.SetActive(true);
        tutorialCanvas.SetActive(true);
        AddCoinCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
    }

}
