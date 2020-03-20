using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnStartOnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "player")
            Destroy(gameObject);
    }
}
