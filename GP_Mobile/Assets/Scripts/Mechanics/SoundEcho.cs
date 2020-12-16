using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEcho : MonoBehaviour
{
    //public GameObject subject;
    public bool scaleInOut;
    public float maxSize;
    public float scaleValue;
    float scaleModifier;
    //public float soundHeight;
    private Vector3 scale;

    private void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        scale = this.gameObject.transform.localScale;
        scaleModifier = 1;
        scale.y = 0.1f;
    }


    public void EchoChange()
    {
        if(Time.timeScale == 1f)
        {
        scale.x += scaleValue * scaleModifier;
        scale.z += scaleValue * scaleModifier;
        this.gameObject.transform.localScale = scale;

        CheckEchoSize();

        }
    }


    private void CheckEchoSize()
    {
        //checking raidius size
        if(scale.x >= maxSize || scale.z >= maxSize || scale.x < 0 || scale.z < 0)
        {
            if (!scaleInOut) Destroy(this.gameObject);
            else scaleModifier *= -1;
        }
    }

}
