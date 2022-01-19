using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] private float lifeDuration;
    void Start()
    {
        Destroy(gameObject, lifeDuration);
    }
}
