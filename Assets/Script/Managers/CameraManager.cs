using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] [Range(0f, 1f)] private float CameraFollowSpeed;
    private Camera mainCam;
    private float camVelocity;
    void Start()
    {
        mainCam = Camera.main;
    }

   
    void LateUpdate()
    {
        var CameraNewPos = mainCam.transform.position;

        mainCam.transform.position = new Vector3(Mathf.SmoothDamp(CameraNewPos.x, target.transform.position.x, ref camVelocity,
            CameraFollowSpeed), CameraNewPos.y, CameraNewPos.z);
    }
}
