using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFoodSpawner : MonoBehaviour
{
    [SerializeField]
    private BodyController bodyController;
    public float SpawnEvery;
    [SerializeField]
    private GameObject SpecialFood;
    [SerializeField]
    private GamePlayCanvasController GamePlayCanvasController;
    private int doneWeight;
    public void Start()
    {
        doneWeight = 0;
    }
    // Update is called once per frame
    void Update()
    {
        int tmp = bodyController.getNumberOfFruitCollected();
        if (tmp != doneWeight && tmp%SpawnEvery == 0)
        {
            FoodSpawner.SpawnFood(SpecialFood, gameObject);
            GamePlayCanvasController.SetTextIndicator("Special Food Spawned!!! Will Disapper in few seconds.");
            doneWeight = tmp;
        }
    }



}
