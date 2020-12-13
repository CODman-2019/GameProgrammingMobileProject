using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    public GameObject[] keys;
    bool closed;
    bool missing;
    int missed;
    public float open;

    private GameObject infoBox;
    Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        infoBox = GameObject.FindGameObjectWithTag("Hint");
        infoText = infoBox.GetComponent<Text>();
        closed = true;
        missing = true;
    }

    //checks if all keys been collected
    void CheckKeys()
    {
        missed = 0;

        for(int i = 0; i < keys.Length; i++)
        {
            //if the key has not been picked up, mark it
            if(!keys[i].GetComponent<Key>().PickedUp())
            {
                missed++;
            }
            //if all the keys are collected, mark false
            if (i == keys.Length - 1 && missed == 0)
            {
                missing = false;
            }
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        //if the gate is not closed, move gate
        if(!closed) transform.Translate( 0,open,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player touches the door
        if (other.CompareTag("Player"))
        {
            //check the keys
            CheckKeys();
            //switch based on the missing variable
            switch( missing)
            {
                //if there are still missing keys, send a message to player
                case (true):
                    if(missed ==  1) infoText.text = "you are missing a key";
                    else infoText.text = "you are missing "+ missed +" keys"  ;
                    break;

                //if not, chang closed to false
                case (false):
                    infoText.text = "door is now opened";
                    closed = false;
                    Player player = GameObject.Find("Player").GetComponent<Player>();
                    player.AddExp(5*keys.Length);
                    break;
            }

            infoBox.GetComponent<GameHintText>().ClearOutText();
        }
    }
}
