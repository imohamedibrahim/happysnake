using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerAndPlanetSetter : MonoBehaviour
{

    [SerializeField]
    private GameObject snakeList;
    [SerializeField]
    private GameObject snakeObjectToSetUnder;
    [SerializeField]
    private GameObject planetObjectToSetUnder;
    [SerializeField]
    private GameObject snakeBodyObject;
    [SerializeField]
    private GameObject planetList;
    [SerializeField]
    private GameObject tutorialPlanet;
    [SerializeField]
    private SettingsCanvasController SettingsReflector;
    
    
    void OnEnable()
    {
        setSnake();
        setPlanet();
        SettingsReflector.Start();
        GameStateHolder.numberOfGamesPlayed = GameStateHolder.numberOfGamesPlayed + 1;
    }

    private void setSnake()
    {
        deleteGameObjects(snakeObjectToSetUnder);
        setGameObject(snakeObjectToSetUnder, snakeList, GameStateHolder.snakeSelected);
        
    }

    private void setPlanet()
    {
        deleteGameObjects(planetObjectToSetUnder);
        setGameObject(planetObjectToSetUnder, planetList, GameStateHolder.planetSelected);
    }

    private void setGameObject(GameObject objectToSetUnder,GameObject GameObject,string toSearch)
    {
        GameObject tmpGameObject = null;
        foreach (Transform tmp in GameObject.transform)
        {
            
            if (tmp.name == toSearch)
            {
                tmpGameObject = Instantiate(tmp.gameObject);
                break;
            }
        }
        tmpGameObject.transform.parent = objectToSetUnder.transform;
        tmpGameObject.transform.localPosition = new Vector3(0, 0, 0);
        tmpGameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        tmpGameObject.transform.localScale = new Vector3(100, 100, 100);
        tmpGameObject.transform.tag = "CanDelete";

        if (toSearch.Contains("snake") || toSearch.Contains("Snake"))
            snakeBodyObject.GetComponent<Renderer>().material = tmpGameObject.GetComponent<Renderer>().material;
        
    }

    private void deleteGameObjects(GameObject objectToSetUnder)
    {

        foreach (Transform i in objectToSetUnder.transform)
        {
            if (i.tag == "CanDelete")
                Destroy(i.gameObject);
        }
    }
}
 