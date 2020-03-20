using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingObject : MonoBehaviour
{
    private Vector3 finalPosition;
    public float smoothnessInMove;
    // Start is called before the first frame update
    void Start()
    {
        finalPosition = transform.localPosition;
        transform.localPosition = getTransformPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition,finalPosition, Time.deltaTime * smoothnessInMove);
    }



    private Vector3 getTransformPosition()
    {
        Vector3 tmp = transform.localPosition * 50 + transform.localPosition;
        return tmp;
    }
}
