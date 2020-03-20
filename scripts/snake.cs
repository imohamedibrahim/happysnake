using System;
using UnityEngine;

public class snake : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private GameObject planet;
    [SerializeField]
    private float smoothnessInMove;
    [SerializeField]
    private float movementDistance;
    [SerializeField]
    private float time_interval;
    private float degreeToTurn = 90.0f;
    float screenSize;
    private Vector3 moveTowards ;

    private String turn;
    private Vector2 startingPos;

    private float max_movement;
    
    private float tmp_time_counter;
    private int counter;
    String previousColliderTag;
    private string currentSide;
    Vector3 nextPosition;
    planetManager planetManager;

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        max_movement = 1f;
        planetManager = new planetManager(transform.gameObject,smoothnessInMove);
        transform.localPosition = new Vector3(0.05f, -0.45f, -0.55f);
        nextPosition = transform.localPosition;
        transform.localRotation = Quaternion.Euler(270, 90, 270);
        currentSide = "frontWall";
        tmp_time_counter = 0;
        previousColliderTag = "fr";

        counter = 0;
    }
    private void controlViaTouch()
    {
        
        foreach (Touch t in Input.touches)
        {
            if(TouchPhase.Began == t.phase)
            {
                computeDirection(t.position);

            }
            
        }
    }

    private void computeDirection(Vector2 tmp)
    {


       
    }

    void Update()
    {
        
        Vector3 currPosition = GetGameObjectPosition();
        controlViaTouch();
        if (Time.deltaTime != 0 && Input.GetKeyUp(KeyCode.LeftArrow))
            turn = "Left";
        if (Time.deltaTime != 0 && Input.GetKeyUp(KeyCode.RightArrow))
            turn = "Right";
        if (Time.deltaTime != 0 && Input.GetKeyUp(KeyCode.UpArrow))
            turn = "Up";
        if (Time.deltaTime != 0 && Input.GetKeyUp(KeyCode.DownArrow))
            turn = "Down";
        tmp_time_counter += Time.deltaTime;
        // camera.transform.LookAt(transform);
        // camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, transform.rotation, Time.deltaTime * smoothnessInMove);
        //planet.transform.rotation = Quaternion.Lerp(planet.transform.rotation, Quaternion.Euler(-transform.right * degreeToTurn), smoothnessInMove * Time.deltaTime);
        //Debug.Log("Player f:" + transform.forward + " r:" + transform.right + " up:" + transform.up);
        if (tmp_time_counter >= time_interval)
        {
          
            nextPosition = transform.position + transform.TransformDirection(Vector3.forward)*movementDistance;

            //Debug.Log("tranform.Direction"+ transform.TransformDirection(new Vector3(0, 0, 1)) + "currentPos "+transform.localPosition+"nextPos"+nextPosition);
            transform.position = Vector3.Lerp(transform.position, nextPosition, smoothnessInMove * Time.deltaTime);

            counter++;
            tmp_time_counter = 0;
        }
        if (turn != "")
        {
            RotateObject(turn);
            turn = "";
            counter = 0;
        }
       // camera.transform.LookAt(transform);
    }

    

  

    private void rotatePlanet()
    {
        Vector3 tmpRightVector = planet.transform.right;
        float tmpAngleToRotate;
        if (Math.Round(tmpRightVector.x) == 0)
        {
            tmpAngleToRotate = planet.transform.rotation.x;
        }else if(Math.Round(tmpRightVector.y) == 0)
        {
            tmpAngleToRotate = planet.transform.rotation.y;
        }
        else
        {
            tmpAngleToRotate = planet.transform.rotation.z;
        }
       // planetManager.RotatePlanet(planet, tmpAngleToRotate + 90);
    }

    private void RotateObject(String direction)
    {

        Debug.Log(direction);
        if (direction == "Left")
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(transform.rotation.x,transform.rotation.y-degreeToTurn,transform.rotation.z),smoothnessInMove*Time.deltaTime);
           // camera.transform.Rotate(transform.up * -degreeToTurn);
        }

        if (direction == "Right") {
            transform.Rotate(transform.up * degreeToTurn);
            camera.transform.Rotate(transform.up * degreeToTurn);
        }

     /**   //if (direction == "Up")
        //{
            float tmpDegreeToTurn = 0f;
            if(currentSide != "topWall" && currentSide != "bottomWall")
            {
                Debug.Log("Up is Working...");
                transform.localRotation= Quaternion.Lerp(transform.localRotation,Quaternion.Euler(-90,transform.localRotation.y, transform.localRotation.z), smoothnessInMove);
            }
            //transform.RotateAround(transform.position, transform.up, tmpDegreeToTurn);
        //}
        //if (direction == "Down") transform.Rotate(transform.up * degreeToTurn);
    **/
    }
   


    private Vector3 GetGameObjectPosition()
    {
        return gameObject.transform.position;
    }
}
