using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmellEnemy : Enemy
{

    NavMeshAgent agent;

    enum states
    {
        sniffing,
        tracking,
        chasing,
        attakcing
    }

    states currentState;


    // Start is called before the first frame update
    void Start()
    {
        range = 50;
        strength = 10;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Sniff()
    {

    }

    void Track()
    {

    }

    void Chase()
    {

    }

    public override void Attack()
    {
        
    }
}
