using System;
using UnityEngine;

public class SpecialFoodProperty : MonoBehaviour
{
    [SerializeField]
    private int foodWeight = 5;
    private int foodWeightCopy = 1;
    [SerializeField]
    private AudioSource foodCollectedAudio;
   
    public void Update()
    {
        foodWeightCopy = ((int)Math.Round(GameStateHolder.difficulty * 15 / 2.5)+1) * foodWeight;
    }

    public void SetFoodWeight(int tmp)
    {
        foodWeight = tmp;
    }

    public int GetFoodWeightCopy()
    {
        return foodWeightCopy;
    }
    public int GetFoodWeight()
    {
        return foodWeight;
    }
}
