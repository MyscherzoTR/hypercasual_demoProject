using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    Vector3 firstPos, endPos;
    [SerializeField] private float playerSpeed = 10.0f, swipeSpeed = 0.05f;

    private void LateUpdate()
    {
        transform.Translate(0, 0, playerSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;

            float distance = endPos.x - firstPos.x;
            transform.Translate(distance * Time.deltaTime * swipeSpeed, 0, 0);
        }

        if (Input.GetMouseButtonUp(0))
        {
            firstPos = Vector3.zero;
            endPos = Vector3.zero;
        }
    }
}
