using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform planet;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Vector3 offsetPosition;
    
    [SerializeField]
    private static float smoothnessInRotation = 10f;

    [SerializeField]
    private bool lookAt = true;

    private bool doesChangeNeeded;
    private float playerRotationTrackerX;
    private float playerRotationTrackerY;
    private void Start()
    {
        doesChangeNeeded = true;
        playerRotationTrackerX = getRoundedValue(player.rotation.eulerAngles.x);
        playerRotationTrackerY = getRoundedValue(player.rotation.eulerAngles.y);

    }

    private float getRoundedValue(float value)
    {
        return (float)Math.Round(value);
    }
   private void Update()
    {
        //Refresh();
        //CameraMove();
        Change();
    }

    public void Change()
    {
        Vector3 tmp = new Vector3((float)Math.Round(player.transform.up.x),(float)Math.Round(player.transform.up.y),(float)Math.Round(player.transform.up.z));
        transform.position = Vector3.Lerp(transform.position,tmp*35, Time.deltaTime*smoothnessInRotation);
        //transform.rotation = Quaternion.Euler(player.forward);
       
       
    }

    int counter = 0;
    public void CameraMove()
    {
        Vector3 tmpOffsetPosition = new Vector3(0, 0, 0);
        Vector3 tmpRight = player.right;
        tmpRight = new Vector3((float)Math.Round(tmpRight.x,0), (float)Math.Round(tmpRight.y,0), (float)Math.Round(tmpRight.z,0));
        tmpOffsetPosition = player.transform.position;
     //   Debug.Log("Player pos:" + tmpOffsetPosition);
        if (tmpRight.x < 0)
        {
            tmpRight.x = 1;
        }
        if (tmpRight.y < 0)
        {
            tmpRight.y = 1;
        }
        if (tmpRight.z < 0)
        {
            tmpRight.z = 1;
        }
        if (tmpOffsetPosition.x < 0 && tmpRight.x != 0)
        {
            tmpRight.x = -1;
        }
        if (tmpOffsetPosition.y < 0 && tmpRight.y != 0)
        {
            tmpRight.y = -1;
        }
        if (tmpOffsetPosition.z < 0 && tmpRight.z != 0)
        {
            tmpRight.z = -1;
        }
        tmpOffsetPosition = tmpOffsetPosition + (tmpRight * offsetPosition.x) ;
       // Debug.Log("1: "+tmpOffsetPosition);
        tmpOffsetPosition = tmpOffsetPosition + player.forward * offsetPosition.x;
       // Debug.Log("2: " + tmpOffsetPosition);
        tmpOffsetPosition = tmpOffsetPosition + player.up * offsetPosition.y;
      //  Debug.Log("3: " + tmpOffsetPosition);
        transform.position = Vector3.Lerp(transform.position,tmpOffsetPosition, Time.deltaTime*smoothnessInRotation);
        //transform.LookAt(player);
        //Debug.Log("Camera Pos: "+transform.position+" player Pos: "+player.position);
        
        counter++;
    }
    private void ChangeCameraPosition()
    {

        
        throw new NotImplementedException();
    }
    public void Refresh()
    {
        if (player == null || planet == null)
        {
            Debug.LogWarning("Missing planet or player!", this);
            return;
        }
        Debug.Log(offsetPosition + "change needed" + doesChangeNeeded);
        // check whether camera movement is needed
        //if ((offsetPosition.x < 0 && player.position.x > 0 || offsetPosition.x > 0 && player.position.x < 0) || (offsetPosition.y < 0 && player.position.y > 0 || offsetPosition.y > 0 && player.position.y < 0) || (offsetPosition.z < 0 && player.position.z > 0 || offsetPosition.z > 0 && player.position.z < 0)) doesChangeNeeded = true;
        // compute position
        Vector3 tmpOffsetPosition = new Vector3(0, 0, 0);
            //   if (player.position.x < 0 && offsetPosition.x > 0) offsetPosition.x = -offsetPosition.x;
            //   if (player.position.y < 0 && offsetPosition.y > 0) offsetPosition.y = -offsetPosition.y;
            //   if (player.position.z < 0 && offsetPosition.z > 0) offsetPosition.z = -offsetPosition.z;
            tmpOffsetPosition.x = Math.Sign(player.position.x) * offsetPosition.x;
            tmpOffsetPosition.y = Math.Sign(player.position.y) * offsetPosition.y;
            tmpOffsetPosition.z = Math.Sign(player.position.z) * offsetPosition.z;
            
            //Debug.Log("Player Pos: " + player.position + "offset pos: " + offsetPosition);
          //  doesChangeNeeded = false;
        //transform.position = Vector3.Lerp(transform.position, player.TransformPoint(tmpOffsetPosition), Time.deltaTime * smoothnessInRotation);
        //transform.LookAt(player.transform);

        // compute rotation
        /**if (lookAt)
        {
            Quaternion targetRotation = Quaternion.LookRotation(planet.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothnessInRotation);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, planet.rotation, Time.deltaTime * smoothnessInRotation);
        }**/
    }
}
