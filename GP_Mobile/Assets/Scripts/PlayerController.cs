﻿
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float Health;

    public Camera view;
    public NavMeshAgent agent;

    public float distance;
    public GameObject heart;
    public Vector3 startPos;

    public float heartRate;
    float heartBeatRange;
    public float hMaxRange;
    public float heartTimer;
    public float beatIncrement;
    //int beatCount;
    public int maxBeatCount;
    bool reduceBeat;
    float space;

    private void Start()
    {
        reduceBeat = false;
        // beatCount = 0;

        startPos = gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
       

        //Debug.Log(message: agent.pathStatus);
        // if the left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            Ray ray = view.ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                space = Vector3.Distance(hit.point, transform.position);

                

                if(/*Physics.Raycast(hit.point, transform.position) && */(Vector3.Distance(hit.point, transform.position) <= distance ))
                {
                    agent.SetDestination(hit.point);
                }

                
            }
        }

        BeatHeart();

    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if(Health < 0)
        {
            //GameManager.control.ToMainMenu();
            SceneManageMent.direction.LoadMainScene();
        }
    }

    public void HealHealth(float heal)
    {
        Health += heal;
    }

    // hearecho code - BeatHeart, CheckBeat, ScaleBeatRange

    //Beat
    void BeatHeart()
    {
        if (!reduceBeat)
        {
            heartBeatRange += beatIncrement;
            // Debug.Log("BEEP");
        }
        else
        {

            heartBeatRange -= beatIncrement;
            // Debug.Log("BOOP");
        }
        CheckBeat();
    }

    void CheckBeat()
    {
        if (heartBeatRange > hMaxRange)
        {
            reduceBeat = true;
        }
        if (heartBeatRange < 0)
        {
            reduceBeat = false;
            //beatCount++;
        }

        ScaleBeatRange();

    }

    void ScaleBeatRange()
    {
        Vector3 temp = transform.localScale;

        temp.x = heartBeatRange;
        temp.y = 0.01f;
        temp.z = heartBeatRange;

        heart.transform.localScale = temp;
    }
}


/// --- unused code
///    IEnumerator HeartBeat()
//{
//    yield return new WaitForSeconds(heartRate);
//    BeatHeart();
//    //StartCoroutine(HeartBeat());
//}
/// 
    ///