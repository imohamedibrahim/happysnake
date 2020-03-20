using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnvCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject EnvObjects;
    
    public int CountRequired;
    private List<GameObject> ListOfEnvObjects = new List<GameObject>();
    private const float ConsPoint = 55f;
    private System.Random r;
    public static List<Vector3> ListOfEnvPresent;
    
    // Start is called before the first frame update
    public void Start()
    {
        AddChildToList(EnvObjects);
        r = new System.Random();
        ListOfEnvPresent = new List<Vector3>();
        ListOfEnvPresent.Add(new Vector3(5f, -45f, -50f));
        SpawnObjects();
        
    }

    private void AddChildToList(GameObject envObjects)
    {
        foreach(Transform i in envObjects.transform)
        {
            ListOfEnvObjects.Add(i.gameObject);
        } 
    }

    private void SpawnObjects()
    {
        
        float SpawnCount = CountRequired - transform.childCount;
        for (int i = 0; i < SpawnCount; i++)
        {
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
                    if (ListOfEnvPresent.Contains(new Vector3(rsign * ConsPoint, r1, r2)))
                    {
                        i--;
                        break;
                    }
                    CreateFood(rsign * ConsPoint, r1, r2);
                    break;
                case 2:
                    if (ListOfEnvPresent.Contains(new Vector3(r1, rsign * ConsPoint, r2)))
                    {
                        i--;
                        break;
                    }
                    CreateFood(r1, rsign * ConsPoint, r2);
                    break;
                case 3:
                    if (ListOfEnvPresent.Contains(new Vector3(r1, r2, rsign * ConsPoint)))
                    {
                        i--;
                        break;
                    }
                    CreateFood(r1, r2, rsign * ConsPoint);
                    break;
            }
        }
    }

    private void CreateFood(float x, float y, float z)
    {
        ListOfEnvPresent.Add(new Vector3(x, y, z));
        GameObject tmpGameObject = pickAnObjectFromGameObjectChildList(EnvObjects);
        GameObject Object = Instantiate(tmpGameObject);
        Object.name = "Spawn_"+ tmpGameObject.name;
        Object.transform.parent = transform;
        Object.transform.localScale = new Vector3(1,1,1);
        CalculateAndAssignRotation(Object,x,y,z);
    }

    public static void CalculateAndAssignRotation(GameObject tmp, float x, float y, float z)
    {
        Quaternion tmpQ = Quaternion.Euler(0, 0, 0);
        float consFloat = 5f;
        Vector3 tmpV = new Vector3(x / 10000, (y - consFloat) / 10000, z / 10000);
        if (Math.Abs(y) > 50)
        {
            if (y < 0)
            {
                tmpQ = Quaternion.Euler(180, 0, 0);
                tmpV = new Vector3(x / 10000, (y + consFloat) / 10000, z / 10000);
            }

        }
        else if (Math.Abs(x) > 50)
        {
            if (x > 0)
            {
                tmpQ = Quaternion.Euler(0, 0, -90);
                tmpV = new Vector3((x - consFloat) / 10000, y / 10000, z / 10000);
            }
            else
            {
                tmpQ = Quaternion.Euler(0, 0, 90);
                tmpV = new Vector3((x + consFloat) / 10000, y / 10000, z / 10000);
            }

        }
        else if (Math.Abs(z) > 50)
        {
            if (z > 0)
            {
                tmpQ = Quaternion.Euler(90, 0, 0);
                tmpV = new Vector3(x / 10000, y / 10000, (z - consFloat) / 10000);
            }
            else
            {
                tmpQ = Quaternion.Euler(-90, 0, 0);
                tmpV = new Vector3(x / 10000, y / 10000, (z + consFloat) / 10000);
            }
        }
        tmp.transform.localRotation = tmpQ;
        tmp.transform.localPosition = tmpV;

    }

    private GameObject pickAnObjectFromGameObjectChildList(GameObject envObjects)
    {

        int objectNumber = (int)r.Next(0, ListOfEnvObjects.Count);
        return ListOfEnvObjects[objectNumber];
    }

}
