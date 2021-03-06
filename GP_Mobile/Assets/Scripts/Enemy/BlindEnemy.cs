﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BlindEnemy : Enemy
{
    //variables
    public Color patrolSense;
    public Color searchSense;
    public Color attackSense;
    Material detectionColor;

    public GameObject[] patrolPoints;
    int patrolIndex;
    NavMeshAgent agent;                     

    public bool findPatrolPoints;
    bool reverse;

    enum States
    {
        patrol,
        search,
        chase,
        attack
    }
    States currentState;

    Vector3 lastHeardSpot;
    public float searchDuration;
    public float rangeDistance;
    Player player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        detectionColor = GetComponent<Renderer>().material;

        if(findPatrolPoints) patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoints");
        currentState = States.patrol;
        agent = GetComponent<NavMeshAgent>();

        patrolIndex = FindNearestPoint();
        agent.SetDestination(patrolPoints[patrolIndex].transform.position);
    }

    //method looks for nearest patrol point
    int FindNearestPoint()
    {
        patrolIndex = 0;
        float pointDistance = Vector3.Distance(transform.position, patrolPoints[patrolIndex].transform.position);
        
        for(int i = 1; i < patrolPoints.Length; i++)
        {
            float nextPointDistance = Vector3.Distance(transform.position, patrolPoints[i].transform.position);
            if (nextPointDistance < pointDistance)
            {
                patrolIndex = i;
                pointDistance = nextPointDistance;
            }
        }

        return patrolIndex;
    }

    // Update is called once per frame
    void Update()
    {
        //switch behaviour based on state
        switch (currentState)
        {
            case (States.patrol): Patrol(); break;
            case (States.search): Search(); break;
            case (States.chase): Chase(); break;
            case (States.attack): Attack(); break;
        }

        //adjust color based on state
        AdjustColor();
    }

    //Patrol method
    public void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].transform.position) < 1.1f)
        {
            AdjustCourse();
            agent.SetDestination(patrolPoints[patrolIndex].transform.position);
        }

    }

    //search method
    public void Search()
    {
        if (Vector3.Distance(transform.position, lastHeardSpot) < 1.1f)
        {
            StartCoroutine(Hear());
        }
    }
    
    //Chase method
    public void Chase()
    {
        
        if(Vector3.Distance(transform.position, player.transform.position) > rangeDistance)
        {
            currentState = States.search;
        }
        else if(Vector3.Distance(transform.position, player.transform.position) < 3.0f)
        {
            currentState = States.attack;
        }

    }

    //Attack method
    public override void Attack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3.0f)
        {
            currentState = States.attack;
            player.TakeDamage(strength / 2);
        }

        if (Vector3.Distance(transform.position, player.transform.position) > rangeDistance)
        {
            currentState = States.search;
            StartCoroutine(Hear());
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }
    }

    // get a new point from the patol index
    void AdjustCourse()
    {
        
            //if the index is on the last point
            if(patrolIndex == patrolPoints.Length - 1)
            {
                //if the enemy is going to loop around
                if (loopPath)
                {
                //setthe index point to before the start point
                    patrolIndex = -1;

                }
                //if it will not loop then activate the reverse
                else
                {
                    reverse = true;
                }
            }
            //if it reaches the first point then flip reverse
            if (patrolIndex == 0) reverse = false;
      
            //if the enemy is going back, go back one point
            if (reverse && !loopPath) patrolIndex--;
            else patrolIndex++;

    }

    //adjust the color of the object
    void AdjustColor()
    {
        switch (currentState)
        {
            case States.patrol:
                detectionColor.color = patrolSense;
                break;
            case States.search:
                detectionColor.color = searchSense;
                break;
            case States.attack:
                detectionColor.color = attackSense;
                break;

        }
    }

    IEnumerator Hear()
    {
        yield return new WaitForSeconds(searchDuration);
        if(currentState != States.attack)
        {
            currentState = States.patrol;
            agent.SetDestination(patrolPoints[patrolIndex].transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //check the collided gameobjects tag
        switch (other.tag)
        {
            //if it is a sound
            // - change state to search
            // - set the last heard spot to that position and set it as a destination
            case "Sound":
                currentState = States.search;
                lastHeardSpot = other.transform.position;
                agent.SetDestination(lastHeardSpot);
                break;

            //if it is the player
            // - change state to Attack
            // - set the playersposition as the destination 
            case "Player":
                currentState = States.attack;
                agent.SetDestination(player.transform.position);
            break;

        }

    }

}
