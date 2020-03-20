using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GamePlayCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private MenuChanger menuChanger;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI specialFoodTimmer;
    [SerializeField]
    private Sprite resumeButtonImage;
    [SerializeField]
    private Sprite pauseButtonImage;
    [SerializeField]
    private GameObject resumeAndPauseButtonObject;
    [SerializeField]
    private TextMeshProUGUI startCounterText;
    [SerializeField]
    private TextMeshProUGUI TextIndicator;
    [SerializeField]
    private int timmer = 100;
    [SerializeField]
    private GoogleMobileAd ad;
    [SerializeField]
    private AudioSource numberCountingAudio;
    private bool start = false;
    private int toStartIn;
    private int tmp = 0;
    public void ResumeAndPause()
    {
        Sprite tmp = resumeAndPauseButtonObject.GetComponent<Image>().sprite;
        if (tmp.name.Contains("resume"))
        {
            resumeAndPauseButtonObject.GetComponent<Image>().sprite = pauseButtonImage;
            Time.timeScale = 1f;
        }
        else
        {
            resumeAndPauseButtonObject.GetComponent<Image>().sprite = resumeButtonImage;
            Time.timeScale = 0f;
        }
    }

    public void Start()
    {
        if(GameStateHolder.numberOfGamesPlayed > 2)
        {
            ad.ShowBannerAd();
        }
        if (GameStateHolder.tutorial != "done")
        {
            Time.timeScale = 1f;
            startCounterText.GetComponent<TextMeshProUGUI>().text = "";
            return;
        }
        start = true;
        toStartIn = 4;
        tmp = timmer;
        //    if(GameStateHolder.tutorial != "done")
        //   {
        //       this.transform.parent.gameObject.SetActive(false);
        //   }
        resumeAndPauseButtonObject.SetActive(false);
    }

    public void Update()
    {
        if (!start)
        {
            return;
        }
        if (tmp <= 0)
        {
            numberCountingAudio.Play();
            tmp = timmer;
            toStartIn--;
            
        }
        tmp--;
        if (toStartIn <= 0)
        {
            Time.timeScale = 1f;
            startCounterText.GetComponent<TextMeshProUGUI>().text = "";
            start = false;
            resumeAndPauseButtonObject.SetActive(true);
            return;
        }
        startCounterText.GetComponent<TextMeshProUGUI>().text = toStartIn.ToString();
    }

    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }

    public void SetSpecialFoodTimmerText(string text)
    {
        specialFoodTimmer.text = text;
    }

    public void StartGameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
        
        this.transform.parent.gameObject.SetActive(false);
    }

    public void SetTextIndicator(string msg)
    {
        TextIndicator.gameObject.SetActive(true);
        TextIndicator.GetComponent<BlurAndMoveAway>().Enable();
        TextIndicator.text = msg;
    }


}
