using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject GamePlayMusic;
    [SerializeField]
    private GameObject[] tinySounds;
    [SerializeField]
    private Toggle themeMusicToggle;
    [SerializeField]
    private Toggle tinyMusicToggle;
    [SerializeField]
    private Toggle vibrationToggle;
    [SerializeField]
    private MenuChanger MenuChanger;
    [SerializeField]
    private AudioSource clickMusic;
    // Start is called before the first frame update
    public void Start()
    {
        themeMusicController();
        tinySoundController();
        vibrationController();
    }

    public void goToMenu()
    {
        MenuChanger.goToMainMenu();
    }

    private void themeMusicController()
    {
        if (GameStateHolder.themeMusic == "True")
            themeMusicToggle.isOn = true;
        else
            themeMusicToggle.isOn = false;
        themeMusicEnableOrDisable(themeMusicToggle.isOn);
    }

    private void tinySoundController()
    {
        if (GameStateHolder.tinyMusic == "True")
            tinyMusicToggle.isOn = true;
        else
            tinyMusicToggle.isOn = false;
        tinyMusicEnableOrDisable(tinyMusicToggle.isOn);
    }

    private void vibrationController()
    {
        if (GameStateHolder.vibrate == "True")
            vibrationToggle.isOn = true;
        else
            vibrationToggle.isOn = false;
        tinyMusicEnableOrDisable(vibrationToggle.isOn);
    }

    // Update is called once per frame
   public void themeMusicEnableOrDisable(bool flag)
    {
        flag = themeMusicToggle.isOn;
        GamePlayMusic.GetComponent<AudioSource>().enabled = flag;
        GameStateHolder.themeMusic = flag.ToString();
        clickMusic.Play();
    }

    public void tinyMusicEnableOrDisable(bool flag)
    {
        
        flag = tinyMusicToggle.isOn;
        foreach(GameObject gameObject in tinySounds)
        {
            gameObject.GetComponent<AudioSource>().enabled = flag;

        }
        clickMusic.Play();
        GameStateHolder.tinyMusic = flag.ToString();
    }

    public void vibrationEnableOrDisable(bool flag)
    {
        flag = vibrationToggle.isOn;
        clickMusic.Play();
        GameStateHolder.vibrate = flag.ToString();
    }
}
