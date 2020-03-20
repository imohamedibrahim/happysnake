using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameStarter : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerObjects;
    [SerializeField]
    private TextMeshProUGUI StartCounterText;
    [SerializeField]
    private float timmer;
    private float tmp;
    private bool start;
    private float ToStartIn;
    // Start is called before the first frame update

    public void LetStop()
    {
        PlayerObjects.SetActive(false);
    }

    public void LetStart()
    {
        start = true;
        Time.timeScale = 1f;
        PlayerObjects.SetActive(true);
        ToStartIn = 3;
        tmp = timmer;
    }

    // Update is called once per frame

    void Update()
    {
        if (!start)
            return;
        tmp--;
        if (tmp <= 0)
        {
            tmp = timmer;
            ToStartIn--;
            
        }

        if(ToStartIn <= 0)
        {
            Time.timeScale = 1f;
            StartCounterText.GetComponent<TextMeshProUGUI>().text = "";
            start = false;
            return;
        }
        StartCounterText.GetComponent<TextMeshProUGUI>().text = ToStartIn.ToString();
    }

    void OnDestroy()
    {
        if (StartCounterText != null)
            StartCounterText.GetComponent<TextMeshProUGUI>().text = "";
    }
}
