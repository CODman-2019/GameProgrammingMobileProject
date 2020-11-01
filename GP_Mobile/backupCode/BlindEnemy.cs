using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class BlindEnemy : Enemy
{
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
    bool scan;
    public float searchDuration;
    public float rangeDistance;
    GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        detectionColor = GetComponent<Renderer>().material;

        if(findPatrolPoints) patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoints");
        currentState = States.patrol;
        agent = GetComponent<NavMeshAgent>();
        scan = false;

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
            case (States.chase): Chase(); break;
            case (States.attack): Attack(); break;
        }
        AdjustColor();
    }
    public void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].transform.position) < 1.1f)
        {
            AdjustCourse();
            agent.SetDestination(patrolPoints[patrolIndex].transform.position);
        }

    }
    public void Search()
    {
        if (Vector3.Distance(transform.position, lastHeardSpot) < 1.1f)
        {
            StartCoroutine(Hear());
        }
    }
    
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

    public override void Attack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3.0f)
        {
            currentState = States.attack;
            player.GetComponent<PlayerController>().TakeDamage(strength / 2);
        }

        if (Vector3.Distance(transform.position, player.transform.position) > rangeDistance)
        {
            currentState = States.search;
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }
    }

    void AdjustCourse()
    {
        if(patrolIndex == patrolPoints.Length - 1) reverse = true;
        if (patrolIndex == 0) reverse = false;

        if (reverse) patrolIndex--;
        else patrolIndex++;
    }

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
        
        switch (other.tag)
        {
            case "Sound":
                currentState = States.search;
                lastHeardSpot = other.transform.position;
                agent.SetDestination(lastHeardSpot);
            break;

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
                currentState = States.attack;
                agent.SetDestination(player.transform.position);
        }
    }

}
