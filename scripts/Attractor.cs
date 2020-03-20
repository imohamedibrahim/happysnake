using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public GameObject tr;
    void FixedUpdate()
    {
        Attract(tr);
    }
    private RaycastHit hit;
    float g = 2f;
    public void Attract(GameObject playerTransform)
    {
        Rigidbody playerRigidBody = playerTransform.GetComponent<Rigidbody>();
        Rigidbody currentRigidBody = this.GetComponent<Rigidbody>();
        Vector3 direction = currentRigidBody.position-playerRigidBody.position;
        float distance = direction.magnitude;
        float forceMagnitude = (currentRigidBody.mass * playerRigidBody.mass) / Mathf.Pow(distance, 2) * g;
        Vector3 force = direction.normalized * forceMagnitude;
        playerRigidBody.AddForce(direction*10);
       
    }
}
