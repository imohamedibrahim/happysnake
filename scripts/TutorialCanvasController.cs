using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject planet;
    [SerializeField]
    private GameObject foodSpawner;
    [SerializeField]
    private GameObject tutorialImageObject;
    [SerializeField]
    private player player;
    [SerializeField]
    private BodyController bodyController;
    [SerializeField]
    private GameObject textIndicatorGameObject;
    [SerializeField]
    private Sprite leftSwipeImage;
    [SerializeField]
    private Sprite rightSwipeImage;
    [SerializeField]
    private GameObject skipTutorialButtonGameObject;
    [SerializeField]
    private GameObject specialFoodGameObject;
    
    private bool showTextDisplay;
    private int steps;
    private int timmer = 0;
    private int stringLength = 0;
    private string stringToDisplay;
    private GameObject planetObject;
    private bool toBeRepeated = false;

    private GameObject getChildWithTag(GameObject planet, string v)
    {
        v = v.ToLower();
        foreach (Transform tmp in planet.transform)
        {
            if (tmp.tag.ToLower() == v)
                return tmp.gameObject;
        }
        return null;
    }


    // Start is called before the first frame update
    void Start()
    {
       if (GameStateHolder.tutorial == "done")
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
            return;
        }
        planetObject = getChildWithTag(planet, "CanDelete");
        planetObject.GetComponent<EnvCreator>().CountRequired = 0;
        DeleteObstacles();
        foodSpawner.GetComponent<FoodSpawner>().CountRequired = 0;
        DeleteFood();
        showTextDisplay = false;
        skipTutorialButtonGameObject.SetActive(true);
        stringToDisplay = "";
    }

    private void DeleteFood()
    {
        foreach(Transform tmp in foodSpawner.transform)
        {
            Destroy(tmp.gameObject);
        }
    }

    private void DeleteObstacles()
    {
        GameObject p = getChildWithTag(planet, "candelete");
        foreach(Transform tmp in p.transform) { 
            Destroy(tmp.gameObject);
        }
    }


    private int NumberOfSpecialFood()
    {
        foreach(Transform tmp in foodSpawner.transform)
        {
            if (tmp.name.Contains("pecial"))
            {
                return 1;
            }
        }
        return 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (showTextDisplay)
        {
            if(timmer % 2 == 0)
            {
                stringLength++;
                if(stringLength <= stringToDisplay.Length )
                    textIndicatorGameObject.GetComponent<TextMeshProUGUI>().text = stringToDisplay.Substring(0, stringLength);
            }
            timmer++;

        }
        if (steps == 0)
        {
            loadImage(leftSwipeImage);
            loadText("swipe Left to turn snake left");
        }
        else if (steps == 1)
        {
            loadImage(rightSwipeImage);
            loadText("swipe right to turn snake right");
        }
        else if (steps == 2 || toBeRepeated)
        {
            if (toBeRepeated)
                loadText("Special food expired, try collecting 7 food to spawn a special food");
            else
                loadText("collect 7 food to spawn a special food!!!");
            foodSpawner.GetComponent<FoodSpawner>().CountRequired = 5;
            foodSpawner.GetComponent<SpecialFoodSpawner>().SpawnEvery = 5;
            tutorialImageObject.SetActive(false);
            
            
        }
        else if (steps == 3)
        {
            loadText("Special food spawn. Try to collect it before timmer shown below ends!!!");
            if(NumberOfSpecialFood() == 0)
            {
                toBeRepeated = true;
            }
            tutorialImageObject.SetActive(false);
        }
        else if (steps == 4 )
        {
            loadText("Good to start");
            Time.timeScale = 0f;
            skipTutorialButtonGameObject.GetComponentInChildren<TextMeshProUGUI>().text = "start game >>>";
        }
        if (steps == 0 && player.getSwipedDirection() == "left")
            steps = 1;
        else if (steps == 1 && player.getSwipedDirection() == "right")
            steps = 2;
        else if (steps == 2 && bodyController.getNumberOfFruitCollected() >= 5)
            steps = 3;
        else if (steps == 3 && bodyController.numberOfSpecialFoodCollected() >= 1)
        {
            steps = 4;
            toBeRepeated = false;
        }
    }

    public void skipTutorial()
    {
        GameStateHolder.tutorial = "done";
        restartGame();
    }

    private void restartGame()
    {
        GameStateHolder.restartClicked = "true";
        SceneManager.LoadScene("Samplescene");
    }

    private void loadText(string v)
    {
        showTextDisplay = true;
        if (stringToDisplay != v)
        {
            stringToDisplay = v;
            stringLength = 0;
        }
    }

    private void loadImage(Sprite image)
    {
        tutorialImageObject.GetComponent<Image>().sprite = image;
        showTextDisplay = true;
    }

    





}
