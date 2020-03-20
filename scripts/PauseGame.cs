using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private GameObject TextView;
    [SerializeField]
    private GameStarter GameStarter;
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private GameObject ScoreText;

    void Start()
    {
        Time.timeScale = 0f;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        TextView.GetComponent<TextMeshPro>().text = "PAUSED";
        StartButton.SetActive(true);
    }
    public void StartIt()
    {
        Time.timeScale = 1f;
        TextView.GetComponent<TextMeshPro>().text = "";
        StartButton.SetActive(false);
    }

    public void InitialStart()
    {
        TextView.GetComponent<TextMeshPro>().text = "";
        StartButton.SetActive(false);
        GameStarter.LetStart();
        ScoreAndSizeTextController(true);
    }


    private void ScoreAndSizeTextController(bool tmp)
    {
        ScoreText.SetActive(tmp);
    }
}
