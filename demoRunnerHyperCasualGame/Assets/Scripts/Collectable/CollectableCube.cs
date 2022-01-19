using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCube : MonoBehaviour
{
    bool toplandiMi;
    float index;
    void Start()
    {
        toplandiMi = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetToplandiMi()
    {
        return toplandiMi;
    }

    public void ToplandiYap()
    {
        toplandiMi = true;
    }

    public void IndexAyarla(float index)
    {
        this.index = index;
    }
}
