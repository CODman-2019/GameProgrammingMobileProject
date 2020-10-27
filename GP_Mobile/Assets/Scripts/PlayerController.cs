
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera view;
    public NavMeshAgent agent;

    public float distance;
    //public bool 

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(message: agent.pathStatus);
        // if the left 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = view.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(Vector3.Distance(hit.point, transform.position) < distance )// move the player+ 2.5f
                agent.SetDestination(hit.point);
            }


        }
    }
}
