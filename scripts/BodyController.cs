using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class BodyController : MonoBehaviour
{
    private int weight = 0;
    private int numberOfFruitCollected = 0;
    private int numberOfSpecialFruitCollected = 0;
    [SerializeField]
    private GameStateHolder GameStateHolder;
    [SerializeField]
    private GamePlayCanvasController gamePlayCanvasController;
    [SerializeField]
    private GameObject BodySegment;
    [SerializeField]
    private GameObject Planet;
    [SerializeField]
    private AudioSource foodCollectedAudioSource;
    [SerializeField]
    private AudioSource snakeGotHitAudioSource;
    [SerializeField]
    private GamePlayCanvasController textIndicator;
    [SerializeField]
    private GamePlayCanvasController game;
    private Animator anim;
    private Vector3 LastPlayerPosition;
    private List<GameObject> PlayerBodySegmentObjectList = new List<GameObject>();
    private Boolean RemoveLastBodySegment;

    public static List<Vector3> PlayerBodySegmentPositionList = new List<Vector3>();

    public int getWeight()
    {
        return weight;
    }

    public int getNumberOfFruitCollected()
    {
        return numberOfFruitCollected;
    }

    public int numberOfSpecialFoodCollected()
    {
        return numberOfSpecialFruitCollected;
    }

    public void Start()
    {
        RemoveLastBodySegment = true;
        LastPlayerPosition = transform.localPosition;
        
    }
   
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "food")
        {
            //StartCoroutine(playFoodCollect());
            FoodSpawner.RemoveFromFoodList(transform.localPosition);
           
            foodCollectedAudioSource.Play();
            Destroy(collision.gameObject);
            weight = collision.transform.GetComponent<SpecialFoodProperty>().GetFoodWeightCopy() + weight;
            if (collision.transform.GetComponent<SpecialFoodProperty>().GetFoodWeight() <= 1)
            {
                numberOfFruitCollected++;
            }
            else
            {
                numberOfSpecialFruitCollected++;
            }
            RemoveLastBodySegment = false;
        }else if(player.startDying && (collision.transform.tag == "bodysegment"  || collision.transform.tag.ToLower() == "obs"))
        {
            if(Time.timeScale != 0)
            {
                StartCoroutine(die());
            }
            else
            {
                Destroy(collision.gameObject);
            }
            

        }
    }
    IEnumerator playFoodCollect()
    {
        if(anim != null)
            anim.SetBool("eat", true);
        yield return new WaitForSeconds(2);
        if(anim != null)
            anim.SetBool("eat",false);
    }

   

    
    IEnumerator die()
    {
        snakeGotHitAudioSource.Play();
        GameStateHolder.currentGamePoint = weight;
        gamePlayCanvasController.SetScoreText("");
        if(GameStateHolder.vibrate == "True")
            Handheld.Vibrate();
        yield return new WaitForSeconds(0.1f);
        gamePlayCanvasController.StartGameOverCanvas();
       
    }
     //Body controller
    private void  BodyLengthHandler()
    {
        //yield return new WaitForSeconds(0f);
        GameObject tmp = Instantiate(BodySegment);
        //tmp.GetComponent<Renderer>().material.color = Color.green;
        tmp.transform.parent = Planet.transform;
        tmp.transform.localPosition = LastPlayerPosition;
        tmp.transform.localRotation = Quaternion.Euler(0, 0, 0);
        tmp.transform.tag = "bodysegment";
        PlayerBodySegmentPositionList.Add(tmp.transform.position);
        PlayerBodySegmentObjectList.Add(tmp);

    }

    public void Update()
    {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 25, Color.red);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 25))
        {
            if(hit.collider.tag == "food")
            {
                StartCoroutine(playFoodCollect());
            }
        }
        Vector3 tmpPlayerLocation = transform.localPosition;
        if(LastPlayerPosition == tmpPlayerLocation || weight == 0)
        {
            return;
        }
        
        BodyLengthHandler();
        LastPlayerPosition = transform.localPosition;
        if (RemoveLastBodySegment)
            RemoveBodySegment();
        else
            RemoveLastBodySegment = true;
        gamePlayCanvasController.SetScoreText(weight.ToString());    
    }

    private void RemoveBodySegment()
    {
        GameObject tmp = PlayerBodySegmentObjectList[0];
        Destroy(tmp);
        PlayerBodySegmentPositionList.RemoveAt(0);
        PlayerBodySegmentObjectList.RemoveAt(0);
    }
}
