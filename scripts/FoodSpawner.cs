using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private const float ConsPoint = 55f;
    private static int tmp = 0;
    public float CountRequired;
    private static List<Vector3> ListOfFoodLoc;
    public GameObject FoodGameObject;
    //public GameObject Planet;
    private GameObject toBeSpawn;
    private ArrayList notToSpawnPositions;

    public static void RemoveFromFoodList(Vector3 tmp)
    {
        ListOfFoodLoc.Remove(tmp);
    }

    void Start()
    {
        ListOfFoodLoc = new List<Vector3>();
        notToSpawnPositions = new ArrayList();
        FoodGameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    void Update()
    {
        if (!CheckFoodIsEnough(CountRequired))
        {
            tmp = 0;
            SpawnFood(FoodGameObject, gameObject);
                
        }
    }

    public static void SpawnFood(GameObject FoodGameObject, GameObject gameObject)
    {
        tmp++;
        if(tmp > 500)
        {
            return;
        }
        System.Random r = new System.Random();
        float raxis = r.Next(1, 4);
        float rsign = r.Next(0, 2);
        if (rsign == 0)
            rsign = -1;
        else
            rsign = 1;
        float r1 = (r.Next(0, 9) * 10 - 45);
        float r2 = (r.Next(0, 9) * 10 - 45);
        switch (raxis)
        {
            case 1:
                if (CheckCannotSpawn(new Vector3(rsign * ConsPoint, r1, r2),gameObject))
                {
                    SpawnFood(FoodGameObject, gameObject);
                    break;
                }
                CreateFood(FoodGameObject, gameObject, rsign * ConsPoint, r1, r2);
                break;
            case 2:
                if (CheckCannotSpawn(new Vector3(r1, rsign * ConsPoint, r2),gameObject))
                {
                    SpawnFood(FoodGameObject, gameObject);
                    break;
                }
                CreateFood(FoodGameObject, gameObject, r1, rsign * ConsPoint, r2);
                break;
            case 3:
                if (CheckCannotSpawn(new Vector3(r1, r2, rsign * ConsPoint),gameObject))
                {

                    SpawnFood(FoodGameObject, gameObject);
                    break;
                }
                CreateFood(FoodGameObject, gameObject, r1, r2, rsign * ConsPoint);
                break;
        }
    }

    private static bool CheckCannotSpawn(Vector3 tmp,GameObject gameObject)
    {
        
        if (!EnvCreator.ListOfEnvPresent.Contains(tmp) && !ListOfFoodLoc.Contains(tmp))
        {
          return false;
        }
        
        return true;
    }

    private static void CreateFood(GameObject FoodGameObject, GameObject planet,float x,float y,float z)
    {
        ListOfFoodLoc.Add(new Vector3(x, y, z)); 
        GameObject Object = Instantiate(FoodGameObject);
        Object.transform.tag = "food";
        Object.transform.parent = planet.transform;
        Object.transform.localScale = new Vector3(100,100,100);
        CalculateAndAssignRotation(Object, x, y, z);
    }

  
    public static void CalculateAndAssignRotation(GameObject tmp, float x, float y, float z)
    {
        Quaternion tmpQ = Quaternion.Euler(0, 0, 0);
        float consFloat = 5f;
        Vector3 tmpV = new Vector3(x / 100, (y - consFloat) / 100, z / 100);
        if (Math.Abs(y) > 50)
        {
            if (y < 0)
            {
                tmpQ = Quaternion.Euler(180, 0, 0);
                tmpV = new Vector3(x / 100, (y + consFloat) / 100, z / 100);
            }

        }
        else if (Math.Abs(x) > 50)
        {
            if (x > 0)
            {
                tmpQ = Quaternion.Euler(0, 0, -90);
                tmpV = new Vector3((x - consFloat) / 100, y / 100, z / 100);
            }
            else
            {
                tmpQ = Quaternion.Euler(0, 0, 90);
                tmpV = new Vector3((x + consFloat) / 100, y / 100, z / 100);
            }

        }
        else if (Math.Abs(z) > 50)
        {
            if (z > 0)
            {
                tmpQ = Quaternion.Euler(90, 0, 0);
                tmpV = new Vector3(x / 100, y / 100, (z - consFloat) / 100);
            }
            else
            {
                tmpQ = Quaternion.Euler(-90, 0, 0);
                tmpV = new Vector3(x / 100, y / 100, (z + consFloat) / 100);
            }
        }
        tmp.transform.localRotation = tmpQ;
        tmp.transform.localPosition = tmpV;

    }

    private Boolean CheckFoodIsEnough(float CountRequired)
    {
        float NumberOfFoodExists = transform.childCount;
        if (CountRequired > NumberOfFoodExists)
            return false;
        return true;
    }
}
