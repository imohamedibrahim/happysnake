using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AddCoinCanvasController : MonoBehaviour
{
    [SerializeField]
    private MenuChanger menuChanger;
    [SerializeField]
    private GoogleMobileAd ad;
    [SerializeField]
    private GameObject TextIndicator;
    [SerializeField]
    private GameObject VideoPlayButton;
    public void GoToMenu()
    {
        menuChanger.goToMainMenu();
    }

    public void PlayRewardedVideo()
    {
        if (ad.CheckRewardedVideoLoaded())
        {
            ad.ShowRewardedVideo();
           
        }
        else
        {
            SetTextIndicator("Unable To Load AD, TRy Again Later");
        }
    }


    public void Update()
    {
        if (!ad.CheckRewardedVideoLoaded())
        {
            SetTextIndicator("Loading Next Ad...");
        }
        VideoPlayButton.SetActive(ad.CheckRewardedVideoLoaded());
    }

    public void SetTextIndicator(string msg)
    {
        TextIndicator.gameObject.SetActive(true);
        TextIndicator.GetComponent<BlurAndMoveAway>().Enable();
        TextIndicator.GetComponent<TextMeshProUGUI>().text = msg;
    }

}
