using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class GameOverCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject highScoreBanner;
    [SerializeField]
    private TextMeshProUGUI currentScoreText;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;
    [SerializeField]
    private MenuChanger MenuChanger;
    [SerializeField]
    private TextMeshProUGUI goldIndicatorText;
    [SerializeField]
    private GameObject retryButton;
    [SerializeField]
    private GameObject menuButton;
    [SerializeField]
    private float SmoothnessInMove;
    [SerializeField]
    private AudioSource coinCountingAudioSource;
    [SerializeField]
    private GoogleMobileAd ad;
    private bool startGoldIndicator;
    private int counter = 0;
    private int timmer = 0;
    private bool startGoldIndicatorMove;
    

    void Start()
    {
        menuButton.SetActive(false);
        retryButton.SetActive(false);
        startGoldIndicatorMove = false;
        Time.timeScale = 0f;
       
        highScoreBanner.SetActive(false);
        if(GameStateHolder.currentGamePoint > 30)
            counter = GameStateHolder.currentGamePoint - 30;
        if (GameStateHolder.currentGamePoint > GameStateHolder.highestCoinGained)
        {
            EnableHighScore(true);
            GameStateHolder.highestCoinGained = GameStateHolder.currentGamePoint;
            highScoreBanner.GetComponentInChildren<TextMeshProUGUI>().text = GameStateHolder.currentGamePoint.ToString();
            bestScoreText.text = "";
            currentScoreText.text = "";
        }
        else
        {
            EnableHighScore(false);
            bestScoreText.text = "HIGH SCORE : " + GameStateHolder.highestCoinGained.ToString();
            currentScoreText.text = GameStateHolder.currentGamePoint.ToString();
        }
        startGoldIndicator = true;
        if (GameStateHolder.numberOfGamesPlayed % 3 == 0)
            ad.ShowIntertrialAd();
        
    }

    private void EnableHighScore(bool tmp)
    {
        bestScoreText.gameObject.SetActive(!tmp);
        currentScoreText.gameObject.SetActive(!tmp);
        highScoreBanner.SetActive(tmp);
    }
    public void Update()
    {
        int tmpInt = GameStateHolder.currentGamePoint;
        if (startGoldIndicator)
        {
            goldIndicatorText.text = "+ " + counter.ToString();

            if (tmpInt == counter)
            {
                coinCountingAudioSource.Play();
                startGoldIndicator = false;
                GameStateHolder.totalCoinCount = GameStateHolder.totalCoinCount + tmpInt;
                startGoldIndicatorMove = true;
                retryButton.SetActive(true);
                menuButton.SetActive(true);
                return;
            }
            counter++;
            
        }
       timmer++;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Samplescene");
    }

    public void RestartGame()
    {
        GameStateHolder.restartClicked = "true";
        ///MenuChanger.startGame();
        SceneManager.LoadScene("Samplescene");
    }

    public void OnDestroy()
    {
     //   ad.RemoveBannerAd();
      //  ad.RemoveRewardedVideoAd();
      //  ad.RemoveInterAd();
    }



}
