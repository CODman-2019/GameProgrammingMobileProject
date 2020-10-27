
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera view;
    public NavMeshAgent agent;

    public float distance;

    public GameObject soundEcho;
    SoundEcho heart;
    public float heartRate;
    public float heartTimer;

    private void Start()
    {
        heart = soundEcho.GetComponent<SoundEcho>();
    }

    // Update is called once per frame
    void Update()
    {
       // StartCoroutine(HeartBeat());

        //Debug.Log(message: agent.pathStatus);
        // if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = view.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //if(Vector3.Distance(hit.point, transform.position) < distance )// move the player+ 2.5f
                agent.SetDestination(hit.point);
            }


        }
    }

    //IEnumerator HeartBeat()
    //{
    //    yield return new WaitForSeconds(heartRate);
    //    StartCoroutine(heart.Echo(heartTimer));
    //}
}
