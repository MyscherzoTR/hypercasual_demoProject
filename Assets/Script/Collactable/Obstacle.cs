using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public delegate void ObstacleShrinkRate(float sayi);
    public static event ObstacleShrinkRate obsShrinkRate;

    [SerializeField] public float shrinkRate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            obsShrinkRate(shrinkRate);
        }
    }
}
