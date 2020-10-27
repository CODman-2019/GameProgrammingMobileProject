using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class BlindEnemy : Enemy
{
    GameObject[] patrolPoints;
    int patrolIndex;
    NavMeshAgent agent;

    bool reverse;

    enum States
    {
        patrol,
        search,
        attack
    }
     States currentState;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        currentState = States.patrol;
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoints");
        agent = GetComponent<NavMeshAgent>();

        patrolIndex = findNearestPoint();
        agent.SetDestination(patrolPoints[patrolIndex].transform.position);
    }

    int findNearestPoint()
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
        switch (currentState)
        {
            case (States.patrol): Patrol(); break;
            case (States.search): Search(); break;
            case (States.attack): Attack(); break;
        }
    }
    public void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].transform.position) < 1.1f)
        {
            AdjustCourse();
            agent.SetDestination(patrolPoints[patrolIndex].transform.position);
        }
    }

    void AdjustCourse()
    {
        if(patrolIndex == patrolPoints.Length - 1) reverse = true;
        if (patrolIndex == 0) reverse = false;

        if (reverse) patrolIndex--;
        else patrolIndex++;
    }

    public override void Search()
    {

    }

    public override void Attack()
    {

    }
}
