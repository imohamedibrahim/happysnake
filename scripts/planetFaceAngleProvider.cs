using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFaceAngleProvider
{
    private GameObject planet;

    public PlanetFaceAngleProvider(GameObject planet)
    {
        this.planet = planet;
    }

    private Vector3 GetWallAngle(string wallName)
    {
        switch (wallName)
        {
            case "bo": return new Vector3(0, 0, 180);
            case "fr": return new Vector3(90, 0, 180);
            case "ba": return new Vector3(270, 90, 270);
            case "ri": return new Vector3(0, 0, 90);
            case "le": return new Vector3(0, 90, 270);
            default: return new Vector3(0,0,0);
        }
    }

    public void RotatePlanetToWall(string wallName,float smoothnessInMove)
    {
        this.planet.transform.rotation = Quaternion.Lerp(this.planet.transform.rotation,Quaternion.Euler(GetWallAngle(wallName)),smoothnessInMove*Time.deltaTime);
    }

}
