using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlurAndMoveAway : MonoBehaviour
{
    [SerializeField]
    private float TimeToFade = 0.005f;
    private float timmer = 0;
    private bool fade = false;
    private bool RunOnStart;
    Color tmp;
    Color Original;

    public void Start()
    {
        RunOnStart = true;
    }

    public void Enable()
    {
        fade = true;

        tmp = this.GetComponent<TextMeshProUGUI>().color;
            this.GetComponent<TextMeshProUGUI>().color = new Color(tmp.r, tmp.g, tmp.b,1);
            RunOnStart = !RunOnStart;

    }

    public void Update()
    {


        if (fade)
        {
            tmp = this.GetComponent<TextMeshProUGUI>().color;
            float t = Mathf.Lerp(tmp.a, tmp.a - 10, TimeToFade);
            this.GetComponent<TextMeshProUGUI>().color = new Color(tmp.r, tmp.g, tmp.b, t);
        }
        if(tmp.a < 0.05f)
        {
            fade = false;
            this.gameObject.SetActive(false);
        }
        
    }


}
