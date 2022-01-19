using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    GameObject player;
    float playerHeight, height, growthRate = 0.25f, shrinkRate;
    private Vector3 ScaleChange;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Obstacle.obsShrinkRate += ObstacleShrinkRate;
    }


    void ObstacleShrinkRate(float shrinkRate)
    {
        this.shrinkRate = shrinkRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        playerHeight = player.transform.localScale.y;

        if (other.gameObject.tag == "Collectable")
        {
            player.transform.position = new Vector3(transform.position.x, transform.position.y + (playerHeight * growthRate)/2, transform.position.z);
            height = (playerHeight * growthRate) + playerHeight;
            ScaleChange = new Vector3(1.0f, height, 1.0f);
            player.transform.localScale = ScaleChange;

            
            other.gameObject.GetComponent<CollectableCube>().ToplandiYap();
            other.gameObject.GetComponent<CollectableCube>().IndexAyarla(height);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            player.transform.position = new Vector3(transform.position.x, transform.position.y - (playerHeight * shrinkRate) / 2, transform.position.z);
            height = playerHeight - (playerHeight * shrinkRate);
            ScaleChange = new Vector3(1.0f, height, 1.0f);
            player.transform.localScale = ScaleChange;
            Destroy(other.gameObject);
        }
    }
}
