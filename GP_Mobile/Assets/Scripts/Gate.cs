using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    public GameObject[] keys;
    public Text messasge;
    bool closed;
    bool missing;
    int missed;
    public float open;

    // Start is called before the first frame update
    void Start()
    {
        closed = true;
        missing = true;
    }

    void CheckKeys()
    {
        missed = 0;
        for(int i = 0; i < keys.Length; i++)
        {
            if(!keys[i].GetComponent<Key>().PickedUp())
            {
                missed++;
            }

            if (i == keys.Length - 1 && missed == 0)
            {
                missing = false;
            }
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        if(!closed) transform.Translate(open, 0,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckKeys();
            switch( missing)
            {
                case (true):
                    if(missed ==  1) messasge.text = "you are missing a key";
                    else messasge.text = "you are missing "+ missed +" keys"  ;
                    break;

                case (false):
                    messasge.text = "door is now opened";
                    closed = false;
                    break;
            }
        }
    }
}
