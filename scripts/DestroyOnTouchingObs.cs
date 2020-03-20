using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouchingObs : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "bodysegment")
        {
            Destroy(gameObject);
        }
    }
}
