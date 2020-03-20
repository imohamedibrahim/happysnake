using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [SerializeField]
    private float CloudSpeed = 0.4f;
    [SerializeField]
    private float CoverDistance = 50f;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 finalPosition = new Vector3(initialPosition.x + CoverDistance, initialPosition.y, initialPosition.z);
        if(transform.position.x >= finalPosition.x - 10)
        {
            transform.position = new Vector3(initialPosition.x-100,initialPosition.y,initialPosition.z);
            return;
        }
        transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * CloudSpeed);
    }
}
