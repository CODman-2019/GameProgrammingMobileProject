using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEcho : MonoBehaviour
{
    //public GameObject subject;

    float currentEcho;
    public float maxEcho;

    // Start is called before the first frame update
    void Start()
    {
        currentEcho = 0;
    }


    public IEnumerator Echo(float secs)
    {
        yield return new WaitForSeconds(secs);
        currentEcho++;
        CheckEcho();
    }

    private void CheckEcho()
    {
        if (currentEcho > maxEcho)
        {
            currentEcho = 0;
        }

        ScaleEcho();
    }

    private void ScaleEcho()
    {
        Vector3 temp = transform.localScale;

        temp.x = currentEcho;
        temp.z = currentEcho;

        transform.localScale = temp;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
        
    //}
}
