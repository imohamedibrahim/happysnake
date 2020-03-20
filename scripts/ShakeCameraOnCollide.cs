using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCameraOnCollide : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField] private float shake_intensity;
    [SerializeField] private float shake_decay;
    private float tmp_shake_intensity;
    private float tmp_shake_decay;
    private Vector3 originPosition;
    private Quaternion originRotation;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet")
        {
            Shake();   
        }
    }

    public void Update()
    {
        if (tmp_shake_intensity > 0)
        {
            cam.transform.position = originPosition + Random.insideUnitSphere * tmp_shake_intensity;
            cam.transform.rotation = new Quaternion(
            originRotation.x + Random.Range(-tmp_shake_intensity, tmp_shake_intensity) * .2f,
                                     originRotation.y + Random.Range(-tmp_shake_intensity, tmp_shake_intensity) * .2f,
                                     originRotation.z + Random.Range(-tmp_shake_intensity, tmp_shake_intensity) * .2f,
                                     originRotation.w + Random.Range(-tmp_shake_intensity, tmp_shake_intensity) * .2f);
            tmp_shake_intensity -= tmp_shake_decay;
        }
    }

    private void Shake()
    {
        originPosition = cam.transform.position;
        originRotation = cam.transform.rotation;
        tmp_shake_intensity = shake_intensity;
        tmp_shake_decay = shake_decay;
    }
}
