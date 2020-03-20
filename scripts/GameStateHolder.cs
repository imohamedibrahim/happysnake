using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHolder : MonoBehaviour
{
    private static int currentCoinCount = 0;
    public void Start()
    {
        PlayerPrefs.SetString("DefaultSnake", "unlocked");
        PlayerPrefs.SetString("DefaultPlanet", "unlocked");
        
    }
    public static string snakeSelected
    {
        get
        {
            if (PlayerPrefs.HasKey("SnakeSelected"))
                return PlayerPrefs.GetString("SnakeSelected");
            return "DefaultSnake";
        }
        set
        {
            PlayerPrefs.SetString("SnakeSelected", value);
        }
    }

    public static string planetSelected
    {
        get
        {
            if (PlayerPrefs.HasKey("PlanetSelected"))
                return PlayerPrefs.GetString("PlanetSelected");
            return "DefaultPlanet";
        }
        set
        {
            PlayerPrefs.SetString("PlanetSelected", value);
        }
    }

    public static int totalCoinCount
    {
        get
        {
            if (PlayerPrefs.HasKey("TotalCoinCount"))
                return PlayerPrefs.GetInt("TotalCoinCount");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("TotalCoinCount", value);
        }
    }

    public static int currentGamePoint
    {
        get
        {
            return currentCoinCount;
        }
        set
        {
            currentCoinCount = value;
        }
    }

    public static int highestCoinGained
    {
        get
        {
            if (PlayerPrefs.HasKey("HighestCoinGained"))
                return PlayerPrefs.GetInt("HighestCoinGained");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("HighestCoinGained",value);
        }
    }

    public static float difficulty
    {
        get
        {
            if (PlayerPrefs.HasKey("Difficulty"))
                return PlayerPrefs.GetFloat("Difficulty");
            return 0.2f;
        }
        set
        {
            PlayerPrefs.SetFloat("Difficulty",value);
        }
    }

    public static string restartClicked
    {
        get
        {
            if (PlayerPrefs.HasKey("RestartClicked"))
                return PlayerPrefs.GetString("RestartClicked");
            return "false";
        }
        set
        {
            PlayerPrefs.SetString("RestartClicked", value);
        }
    }

    public static string getObjectState(string _objectName)
    {
        string objectState = "locked";
        if (PlayerPrefs.HasKey(_objectName))
        {
            objectState = PlayerPrefs.GetString(_objectName);
        }
        return objectState;
    }

    public static void setObjectStateToUnlock(string _objectName)
    {
        PlayerPrefs.SetString(_objectName, "unlocked");
    }

    public static string themeMusic
    {
        get {
            string _object = "themeMusic";
            if (PlayerPrefs.HasKey(_object)){
                return PlayerPrefs.GetString(_object);
            }
            return "True";
        }
        set
        {
            PlayerPrefs.SetString("themeMusic", value);
        }

    }
    public static string tinyMusic
    {
        get {
            string _object = "tinyMusic";
            if (PlayerPrefs.HasKey(_object)){
                return PlayerPrefs.GetString(_object);
            }
            return "True";
        }
        set
        {
            PlayerPrefs.SetString("tinyMusic", value);
        }
     }


    public static int numberOfGamesPlayed
    {
        get{
            if(PlayerPrefs.HasKey("numberOfGamesPlayed"))
                return PlayerPrefs.GetInt("numberOfGamesPlayed");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("numberOfGamesPlayed", value);
        }
    }
     public static string vibrate
    {
        get {
            string _object = "vibrate";
            if (PlayerPrefs.HasKey(_object)){
                return PlayerPrefs.GetString(_object);
            }
            return "True";
        }
        set
        {
            PlayerPrefs.SetString("vibrate", value);
        }
}
    

    public static string tutorial
    {
        get
        {
            if (!PlayerPrefs.HasKey("tutorial"))
            {
                return "notdone";
            }
            return "done";
        }

        set
        {
            PlayerPrefs.SetString("tutorial", value);
        }
    }

    
}
