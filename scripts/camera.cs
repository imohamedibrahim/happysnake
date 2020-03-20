using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class camera : MonoBehaviour
{
    [SerializeField]
    private GameObject leftC;
    [SerializeField]
    private GameObject rightC;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject planet;
    private Quaternion nextCameraQuaternion;
    private Vector3 nextCameraPosition;
    [SerializeField]
    private float smoothnessInRotation;
    [SerializeField]
    private float smoothnessInMove;

    // Start is called before the first frame update
    void Start()
    {
        nextCameraQuaternion = transform.transform.rotation;
        nextCameraPosition = transform.position;
    }
    private void ComputeRotation()
    {
        Vector3 tmpRightPosition = new Vector3(115, transform.position.y, transform.position.z);
        Quaternion tmpRightQuaternion = Quaternion.Euler(25, -23, -6);
        Vector3 tmpLeftPosition = new Vector3(-115, transform.position.y, transform.position.z);
        Quaternion tmpLeftQuaternion = Quaternion.Euler(25, 23, 6);

        float tmpL = Math.Abs(Vector3.Distance(leftC.transform.position, player.transform.position));
        float tmpR = Math.Abs(Vector3.Distance(rightC.transform.position, player.transform.position));

        if (tmpL < tmpR)
        {
            //Debug.Log("leftN");
            nextCameraQuaternion = tmpLeftQuaternion;
            nextCameraPosition = tmpLeftPosition;
        }
        //nextPlanetQuaternion = Quaternion.Euler(nextPlanetQuaternion.eulerAngles.x, -nextPlanetQuaternion.eulerAngles.y, nextPlanetQuaternion.eulerAngles.z);
        else if (tmpL > tmpR) { 
        //Debug.Log("rightN");
        nextCameraQuaternion = tmpRightQuaternion;
        nextCameraPosition = tmpRightPosition;
    }     //nextPlanetQuaternion = Quaternion.Euler(nextPlanetQuaternion.eulerAngles.x, -nextPlanetQuaternion.eulerAngles.y, nextPlanetQuaternion.eulerAngles.z);
       
    }
    // Update is called once per frame
    void Update()
    {
        ComputeRotation();
        transform.position = Vector3.Lerp(transform.position, nextCameraPosition, Time.deltaTime * smoothnessInMove);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextCameraQuaternion, Time.deltaTime * smoothnessInRotation);
       // transform.LookAt(planet.transform);
    }
}
