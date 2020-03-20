using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommonCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameStateHolder gameStateHolder;
    [SerializeField]
    private TextMeshProUGUI goldCoinText;
    [SerializeField]
    private GameObject addButton;
    [SerializeField]
    private GameObject MainMenuCanvas;
    void Update()
    {
        if (MainMenuCanvas.activeSelf)
            addButton.SetActive(true);
        else
            addButton.SetActive(false);
        goldCoinText.text = GameStateHolder.totalCoinCount.ToString();
    }
}
