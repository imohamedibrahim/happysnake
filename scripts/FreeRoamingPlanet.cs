using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRoamingPlanet : MonoBehaviour
{
    [SerializeField]
    private float SmoothnessInRotation=100f;
    private float timmer = 0;
    [SerializeField]
    private float ChangeEvery;
    private Quaternion NextQuaternion;

    void Start()
    {
        //ChangeEvery = (Random.value)*100;
        NextQuaternion = Random.rotation;
    }

    void Update()
    {
        if(ChangeEvery < timmer)
        {
            NextQuaternion = Random.rotation;
            timmer = 0;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, NextQuaternion, Time.deltaTime * SmoothnessInRotation);
        timmer++;
    }
}
