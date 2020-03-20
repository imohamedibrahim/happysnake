using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float smoothnessInMove;


    public planetManager(GameObject player,float smoothnessInMove)
    {
        this.player = player;
        this.smoothnessInMove = smoothnessInMove;
    }
    void Update()
    {
        MovePlanet();
    }

    private void MovePlanet()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 tmpPlayerForward = player.transform.forward;
        float forwardValue =  ReturnNonZeroValuePosition(playerPosition,tmpPlayerForward);
        float angleToRotate = getAngleToRotate(forwardValue);
        RotatePlanet(this.gameObject,angleToRotate);
    }

    private Vector3 getFinalRotationAngle(GameObject planet,float angleToRotate, Vector3 tmpPlanetRotation)
    {
        Vector3 planetRotationPrev = planet.transform.rotation.eulerAngles;
        angleToRotate = (tmpPlanetRotation.x+tmpPlanetRotation.y+tmpPlanetRotation.z)*-angleToRotate;

        if(Math.Round(tmpPlanetRotation.x) != 0)
        {
            float tmpAngle = (float)Math.Floor(planetRotationPrev.x / 90) * 90;
            Debug.Log(tmpAngle);
            return new Vector3(angleToRotate,planetRotationPrev.y,planetRotationPrev.z);
        }
        else if(Math.Round(tmpPlanetRotation.y) != 0)
        {
            return new Vector3(planetRotationPrev.x, angleToRotate, planetRotationPrev.z);
        }
        return new Vector3(planetRotationPrev.x, planetRotationPrev.y, angleToRotate);
    }

    private float getAngleToRotate(float forwardValue)
    {
        forwardValue = ((float)Math.Round(forwardValue*10)) + 45;
        //Debug.Log(forwardValue);
        forwardValue = forwardValue / 10;
        return forwardValue * 5;
    }

    public float ReturnNonZeroValuePosition(Vector3 playerPosition,Vector3 tmpPlayerForward)
    {
        
        if (Math.Round(tmpPlayerForward.x) != 0)
        {
            return playerPosition.x * tmpPlayerForward.x;
        }
        else if (Math.Round(tmpPlayerForward.y) != 0)
        {
            return playerPosition.y * tmpPlayerForward.y;
        }
        return playerPosition.z * tmpPlayerForward.z;
    }

    public void RotatePlanet(GameObject planet,float angleToRotate)
    {
        Vector3 tmpPlanetRotation = player.transform.right;
        tmpPlanetRotation = getFinalRotationAngle(planet,angleToRotate, tmpPlanetRotation);
        planet.transform.rotation = Quaternion.Lerp(planet.transform.rotation, Quaternion.Euler(tmpPlanetRotation), Time.deltaTime * smoothnessInMove);
    }
}
