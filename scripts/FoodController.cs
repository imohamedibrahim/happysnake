using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private int size = 0;
    [SerializeField]
    private GameObject BodySegment;

    public void Update()
    {
        
    }
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "food")
        {
            Destroy(collision.gameObject);
            size++;
            StartCoroutine(IncreaseLength());
        }
    }

    IEnumerator IncreaseLength()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject tmp = Instantiate(BodySegment);
        tmp.transform.localPosition = new Vector3(0,0,-size);
    }





}
