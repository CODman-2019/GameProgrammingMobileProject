using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float range;
    public float strength;

    public bool loopPath;


    public abstract void Attack(); // enemy attacks player
}
