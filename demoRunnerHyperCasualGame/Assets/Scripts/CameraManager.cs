using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float smoothSpeed = 0.15f;
    private Vector3 offset;
    void Start()
    {
        offset = this.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        this.transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z) + offset, Time.deltaTime);
    }
}
