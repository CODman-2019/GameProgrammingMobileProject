using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundEcho : MonoBehaviour
{
    //public GameObject subject;

    public float maxEchoSize; 

    public abstract IEnumerator Echo();

    public abstract void CheckEcho();

    public abstract void ScaleEcho();

}
