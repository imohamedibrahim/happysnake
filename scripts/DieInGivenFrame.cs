using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DieInGivenFrame : MonoBehaviour
{
    [SerializeField]
    public float LifeOfObject = 100;
    [SerializeField]
    private GamePlayCanvasController gamePlayCanvasController;

    void Start()
    {
        if (gamePlayCanvasController == null)
            Debug.Log("Some fields are unassigned");
        gamePlayCanvasController.SetSpecialFoodTimmerText("");
    }

    void Update()
    {
        if (transform.tag == "food" && Time.deltaTime != 0) {
            gamePlayCanvasController.SetSpecialFoodTimmerText(LifeOfObject.ToString());
            LifeOfObject--;
            if (0 > LifeOfObject)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
       if (gamePlayCanvasController != null)
            gamePlayCanvasController.SetSpecialFoodTimmerText("");
    }
}
