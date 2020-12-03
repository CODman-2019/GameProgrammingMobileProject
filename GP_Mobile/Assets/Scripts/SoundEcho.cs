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
    public float soundHeight;
    private Vector3 scale;

    private void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        scale = this.gameObject.transform.localScale;
        scale.y = soundHeight;
    }
    
    public void Activate()
    {
        if (scaleInOut)
        {
            
        }
    }

    private void echo()
    {
        scale.x += scaleValue;
        scale.z += scaleValue;
        this.gameObject.transform.localScale = scale;

        CheckEchoSize();
    }

    private void CheckEchoSize()
    {
        //for raidius
        if(scale.x > maxSize / 2)
        {

        }
    }
}
