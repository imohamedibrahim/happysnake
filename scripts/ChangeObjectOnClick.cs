using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChangeObjectOnClick : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectContainingListToShow;
    public static float SmoothnessInMove = 0.1f;
    [SerializeField]
    private GameObject DownButton;
    [SerializeField]
    private GameObject UpButton;
    [SerializeField]
    private MenuChanger MenuChanger;
    [SerializeField]
    private TextMeshProUGUI select;
    [SerializeField]
    private GameObject SelectButton;
    [SerializeField]
    private GameObject LockButton;
    [SerializeField]
    private GameObject lockImage;
    [SerializeField]
    private GameObject TextIndicator;
    [SerializeField]
    private GameObject addCoinImage;
    [SerializeField]
    private GameObject videoButton;
    [SerializeField]
    private GoogleMobileAd googleMobileAd;
    [SerializeField]
    private GameObject MenuButton;
    private int current;
    private Vector3 NextPosition;
    private List<GameObject> listOfObjects;

    void Start()
    {
        current = 0;
        UpButton.SetActive(false);
        DownButton.SetActive(true);
        addCoinImage.SetActive(false);
        MenuButton.SetActive(true);
        NextPosition = ObjectContainingListToShow.transform.localPosition;
        listOfObjects = new List<GameObject>();
        updateListOfObjects();
        updateSelectAndLockButton(listOfObjects[current]);
    }

    private void updateListOfObjects()
    {
        foreach(Transform i in ObjectContainingListToShow.transform)
        {
            listOfObjects.Add(i.gameObject);
        }
    }
    
    public void ShowNextObject()
    {
        if (CheckedCanContinue(current+1))
        {
            current = current + 1;
            UpButton.SetActive(true);
            if (! CheckedCanContinue(current + 1)) DownButton.SetActive(false);
            NextPosition = NextPosition + new Vector3(0, 1000, 0);
        }
        updateSelectAndLockButton(listOfObjects[current]);
    }

    private bool CheckedCanContinue(int tmp)
    {

        if (tmp < 0 || tmp >= ObjectContainingListToShow.transform.childCount)
        {
            return false;
        }
        return true;
    }

    public void ShowPrevObject()
    {
        if (CheckedCanContinue(current - 1))
        {
            current = current - 1;
            DownButton.SetActive(true);
            if (!CheckedCanContinue(current - 1)) UpButton.SetActive(false);
            NextPosition = NextPosition + new Vector3(0, -1000, 0); ;
        }
        
        updateSelectAndLockButton(listOfObjects[current]);
    }

    private void updateSelectAndLockButton(GameObject currentObject)
    {
        string name = currentObject.name;
        if(GameStateHolder.getObjectState(name) == "locked")
        {
            SetSelectButton(false);
            LockButton.GetComponentInChildren<TextMeshProUGUI>().text = currentObject.GetComponent<ObjectProperties>().objectPrice.ToString();
        }else if(name == GameStateHolder.snakeSelected || name == GameStateHolder.planetSelected)
        {
            SetSelectButton(true);
            select.text = "SELECTED";
            
        }
        else
        {
            SetSelectButton(true);
            select.text = "SELECT";
        }
    }

    private void SetSelectButton(bool _boo)
    {
        SelectButton.SetActive(_boo);
        LockButton.SetActive(!_boo);
        lockImage.SetActive(!_boo);
    }
    public void Update()
    {
        ObjectContainingListToShow.transform.localPosition = Vector3.Lerp(ObjectContainingListToShow.transform.localPosition, NextPosition,SmoothnessInMove);
        if (addCoinImage.activeSelf)
        {
            bool tmp = googleMobileAd.CheckRewardedVideoLoaded();
            videoButton.SetActive(tmp);
            if (!tmp)
            {
                showInTextIndicator("LOADING AD...");
            }
        }
    }

    public void saveSnakeSelected()
    {
        GameStateHolder.snakeSelected = listOfObjects[current].name;
        showInTextIndicator("SELECTED");
        updateSelectAndLockButton(listOfObjects[current]);
    }

    public void savePlanetSelected()
    {
        GameStateHolder.planetSelected = listOfObjects[current].name;
        updateSelectAndLockButton(listOfObjects[current]);
        showInTextIndicator("SELECTED");
    }

    public void goToMenu()
    {
        MenuChanger.goToMainMenu();
    }

    public void unlockObject()
    {
        string _curr = listOfObjects[current].name;
        int _price = listOfObjects[current].GetComponent<ObjectProperties>().objectPrice;
        if(GameStateHolder.getObjectState(_curr) == "locked" && GameStateHolder.totalCoinCount >= _price )
        {
            GameStateHolder.setObjectStateToUnlock(_curr);
            GameStateHolder.totalCoinCount = GameStateHolder.totalCoinCount - _price;
            SetSelectButton(true);
            select.text = "SELECT";
            showInTextIndicator("UNLOCKED :)");
        }
        else
        {
            showInTextIndicator("INSUFFICIENT GOLD");
            addCoinImage.SetActive(true);
            DownButton.SetActive(false);
            UpButton.SetActive(false);
            MenuButton.SetActive(false);
        }
    }


    private void showInTextIndicator(string msg)
    {
        TextIndicator.SetActive(true);
        TextIndicator.GetComponent<TextMeshProUGUI>().text = msg;
        TextIndicator.GetComponent<BlurAndMoveAway>().Enable();
    }

    public void closeAddCoinImage()
    {
        addCoinImage.SetActive(false);
        DownButton.SetActive(true);
        UpButton.SetActive(true);
        MenuButton.SetActive(true);
    }

    public void playVideoAd()
    {
        if (googleMobileAd.CheckRewardedVideoLoaded())
        {
            googleMobileAd.ShowRewardedVideo();
        }
    }
  
}
