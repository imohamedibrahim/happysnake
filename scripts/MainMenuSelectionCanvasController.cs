using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MainMenuSelectionCanvasController : MonoBehaviour
{
    [SerializeField]
    private MenuChanger MenuChanger;
    [SerializeField]
    private GameObject noMusicGameobject;
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private AudioSource clickedAudio;
    
    public void GoToSettingMenu()
    {
        clickedAudio.Play();
        MenuChanger.goToSettingMenu();
    }

    public void GoToSnakeSelection()
    {
        clickedAudio.Play();
        MenuChanger.changeToSnakeMenu();
    }
   
    public void GoToPlanetSelection()
    {
        clickedAudio.Play();
        MenuChanger.changeToPlanetMenu();
    }

    public void DifficultySliderOnChanged(float f)
    {
        
            GameStateHolder.difficulty = f;
        
       
        
    }

    public void StartGame()
    {
        //clickedAudio.Play();
        MenuChanger.startGame();
    }

    public void MusicButtonClicked()
    {
        bool enabled = noMusicGameobject.activeSelf;
        EnableMusic(!enabled);
        noMusicGameobject.SetActive(!enabled);
    }

    private void EnableMusic(bool v)
    {
       
    }

    public void AddCoinButtonClicked()
    {
        MenuChanger.goToAddCoinCanvas();
       
    }
    public void Start()
    {
        highScoreText.text = "High score: " + GameStateHolder.highestCoinGained.ToString();
        slider.value = GameStateHolder.difficulty;
    }

    
}
