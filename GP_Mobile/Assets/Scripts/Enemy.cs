using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class -- BAD IDEA
public abstract class Enemy : MonoBehaviour
{
    public float range;
    public float strength;

    public bool loopPath;


    public abstract void Attack(); // enemy attacks player
}
