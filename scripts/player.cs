using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player : MonoBehaviour
{
    [SerializeField]
    private GameObject Planet; 
    public float TimeInterval = 0.5f;
    [SerializeField]
    private float MovementDistance = 1f;
    [SerializeField]
    private float SmoothnessInRotation = 10;
    private float timeCounter = 0;
    private Vector2 startingPos;
    private Vector3 nextPosition;
    private string nextMove = "";
    private Quaternion nextPlanetQuaternion;
    private string previousColliderTag;
    private Vector3 nextPlanetRotation;
    private bool blockControl;
    private Vector2 initialPosition;
    private int currentTouch;
    private float screenWidth;
    private string lastMove;
    public static bool startDying = false;

    public string getSwipedDirection()
    {
        return lastMove;
    }
    void Start()
    {
        initialPosition = new Vector2(0, 0);
         blockControl = false;
        startDying = false;
        screenWidth = Screen.width;
        nextPlanetRotation = Planet.transform.rotation.eulerAngles;
        nextPlanetQuaternion = Planet.transform.rotation;
        previousColliderTag = "fr";
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.localPosition = new Vector3(0.05f, -0.45f, -0.55f);
        transform.localRotation = Quaternion.Euler(270, 90, 270);
        nextPosition = transform.position;
        lastMove = "";
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        string wallName = collision.gameObject.tag.Substring(0, 2);
        if (collision.gameObject.tag.Contains("Wall") && wallName != previousColliderTag && transform.tag=="Player")
        {
            blockControl = true;
            StartCoroutine(Wait(collision));
            Quaternion wallPosition = collision.transform.rotation;
            transform.rotation = wallPosition;
        }
    }

    private void TurnPlayer(int v)
    {
        nextPosition = transform.position + transform.TransformDirection(Vector3.forward) * MovementDistance*v;
        transform.position = nextPosition;
    }

    IEnumerator Wait(Collider collision)
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 nextPlanetRotation = returnDirectionCompareWithPlayer(NormalizedVector(transform.right)) * -90;
        nextPlanetQuaternion = nextPlanetQuaternion * Quaternion.Euler(nextPlanetRotation.x, nextPlanetRotation.y, nextPlanetRotation.z);
        previousColliderTag = collision.gameObject.tag.Substring(0, 2);
        TurnPlayer(1);
    }

    private Vector3 returnDirectionCompareWithPlayer(Vector3 playerDir)
    {
        if(playerDir == NormalizedVector(Planet.transform.right))
        {
            return GetSignOfNormalizedVector(Planet.transform.right)*new Vector3(1,0, 0);
        }else if(playerDir == NormalizedVector(Planet.transform.up))
        {
            return GetSignOfNormalizedVector(Planet.transform.up) * new Vector3(0, 1, 0);
        }
        return GetSignOfNormalizedVector(Planet.transform.forward) * new Vector3(0, 0, 1);
    }

    private float GetSignOfNormalizedVector(Vector3 tmpVector)
    {
        if (0.5f < Mathf.Abs(tmpVector.x))
        {
            if(tmpVector.x<0) return -1;
            return 1;
        }
        else if (0.5f < Mathf.Abs(tmpVector.y))
        {
            if (tmpVector.y < 0) return -1;
            return 1;
        }
        if (tmpVector.z < 0) return -1;
        return 1;  
    }

    private float returnPlanetRotationAngle()
    {
        Vector3 tmpPlayer = NormalizedVector(transform.forward);
        Vector3 tmpPlanet = NormalizedVector(Planet.transform.forward);
        if(tmpPlanet.x+tmpPlanet.y+tmpPlanet.z != tmpPlayer.x + tmpPlayer.y + tmpPlayer.z)
        {
            return -90f;
        }
        return 90f;
    }

    private Vector3 NormalizedVector(Vector3 tmp)
    {
        Vector3 tmpPlanetRight = tmp;
        if (0.5f < Mathf.Abs(tmpPlanetRight.x))
        {
            return new Vector3(1, 0, 0);
        }else if (0.5f < Mathf.Abs(tmpPlanetRight.y))
        {
            return new Vector3(0, 1, 0);
        }else if (0.5f < Mathf.Abs(tmpPlanetRight.z))
        {
            return new Vector3(0, 0, 1);
        }
        return new Vector3(0, 0, 0);
    }

    private void ControlViaTouch()
    {
        foreach (Touch t in Input.touches)
        {
            if (TouchPhase.Began == t.phase)
            {
                ComputeDirection(t.position);
            }
        }
    }

    private void ComputeDirection(Vector2 tmp)
    {
        if (tmp.x < screenWidth / 2)
            nextMove = "left";
        else
            nextMove = "right";
    }

    private void ControlViaSwipe()
    {
        //Debug.Log(Input.touchCount + "Touch Count");
        foreach (Touch touch in Input.touches)
        {
            if (Input.touchCount > 0)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("begin :" + touch.fingerId);
                    initialPosition = touch.position;
                    currentTouch = touch.fingerId;
                }
                else if (touch.fingerId == currentTouch && (touch.phase == TouchPhase.Moved))
                {
                    Debug.Log("moving :" + touch.fingerId);
                    // get the moved direction compared to the initial touch position
                    var direction = touch.position - initialPosition;

                    var signedDirection = Mathf.Sign(direction.x);
                    if (Math.Abs(direction.x) >= 3)
                    {
                        if (signedDirection < 0)
                        {
                            nextMove = "left";
                        }
                        else
                            nextMove = "right";
                        currentTouch = -1;
                        lastMove = nextMove;
                    }
                    

                }
            }
        }
    }

    private void FixedUpdate()
    {

        TimeInterval = 0.30f - GameStateHolder.difficulty * 0.15f;
        //ControlViaTouch();
        ControlViaSwipe();
        Planet.transform.rotation = Quaternion.Lerp(Planet.transform.rotation, nextPlanetQuaternion, SmoothnessInRotation * Time.deltaTime);
        if (Time.deltaTime != 0 && Input.GetKeyUp(KeyCode.RightArrow))
        {
            nextMove = "right";
        }
        else if (Time.deltaTime != 0 && Input.GetKeyUp(KeyCode.LeftArrow))
        {
            nextMove = "left";
        }
       
        if (timeCounter >= TimeInterval)
        {
            if (blockControl)
            {
                blockControl = false;
            }
            else
            {
                TurnPlayer();
                transform.position = nextPosition;
                startDying = true;
            }
            Planet.transform.rotation = Quaternion.Lerp(Planet.transform.rotation, nextPlanetQuaternion, SmoothnessInRotation * Time.deltaTime);
            timeCounter = 0;
        }
        timeCounter = timeCounter + Time.deltaTime;
        // Body Controller
       
    }
    
    private void TurnPlayer()
    {
        if (nextMove == "" )
        {
            nextPosition = transform.position + transform.TransformDirection(Vector3.forward) * MovementDistance;
            return;
        }
        if (nextMove == "right")
        {
            nextPosition = transform.position + transform.TransformDirection(Vector3.right) * MovementDistance;
            transform.Rotate(0, 90, 0);
            nextPlanetRotation = returnDirectionCompareWithPlayer(NormalizedVector(transform.up)) * 90;
        }
        else 
        if (nextMove == "left")
        {
            nextPosition = transform.position + transform.TransformDirection(Vector3.left) * MovementDistance;
            transform.Rotate(0, -90, 0);
            nextPlanetRotation = returnDirectionCompareWithPlayer(NormalizedVector(transform.up))* -90;
        }
        nextPlanetQuaternion = nextPlanetQuaternion * Quaternion.Euler(nextPlanetRotation.x, nextPlanetRotation.y, nextPlanetRotation.z);
        nextMove = "";
        
    }
   
}
