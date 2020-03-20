using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvasRenderer : MonoBehaviour
{
    [SerializeField]
    private GameObject GameOverCanvas;
    [SerializeField]
    private GameObject StateHolder;

    public void Start()
    {
        GameOverCanvas.SetActive(false);
    }
    public void GameOver()
    {
        GameOverCanvas.SetActive(true);
        
    }
}
