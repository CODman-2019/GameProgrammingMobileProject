using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool interactionNeeded;
    public bool flipSwitch;

    //public bool deactivate;
    public Color off;
    public Color on;

    public Material switchColor;
    bool playerDetected;
    bool activated;

    // Start is called before the first frame update
    void Start()
    {
        switchColor = GetComponent<Renderer>().material;
        playerDetected = false;
        activated = false;
        switchColor.color = off;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void FlipSwitch()
    {
        if(interactionNeeded && playerDetected)
        {
            if (!activated)
            {
                activated = true;
                switchColor.color = on;
            }

            else if (activated) 
            {
                activated = false;
                switchColor.color = off;
            } 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;

            if (!interactionNeeded)
            {
                activated = true;
                switchColor.color = on;

                if (activated && flipSwitch) activated = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerDetected = false;

    }
}
