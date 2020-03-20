using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteFoodOnTouch : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "food")
        {
            Destroy(other.gameObject);
        }
    }
}
