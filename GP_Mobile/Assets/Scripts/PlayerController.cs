using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public Camera view;
    public NavMeshAgent agent;

    public float distance;
    public GameObject heart;
    SoundEcho heartBeat;
    public Vector3 startPos;

    public Animator playerAnimator;
    float space;

    private void Start()
    {
        //Load();
        heartBeat = heart.GetComponent<SoundEcho>();

        //reduceBeat = false;
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

                if((Vector3.Distance(hit.point, transform.position) <= distance ))
                {
                    agent.SetDestination(hit.point);
                    playerAnimator.SetBool("moving", true);
                }

            }
        }
        
        if(agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            playerAnimator.SetBool("moving", false);
        }

        heartBeat.EchoChange();
        //BeatHeart();

    }




}

    //public float heartRate;
    //float heartBeatRange;
    //public float hMaxRange;
    //public float heartTimer;
    //public float beatIncrement;
    //public int maxBeatCount;
    //bool reduceBeat;
    //[HideInInspector]
    //public int rank;
    //[HideInInspector]
    //public int currentExp;
    //[HideInInspector]
    //public int nextRank;
    //public int highestLevel;
    //public int scraps;
    //public int meds;
//Data
//[Serializable]
//class Data
//{
//    public int currentRank;
//    public int currentExp;
//    public int currentNextRank;

//    public int currentLevel;
//    public int currentScraps;
//    public int currentMeds;
//}
/// --- unused code
///    IEnumerator HeartBeat() // Save/Load methods
//public void Save()
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");

    //    Data data = new Data
    //    {
    //        //data being stored
    //        currentLevel = highestLevel,
    //        currentScraps = scraps,
    //        currentMeds = meds,
    //        currentRank = rank,
    //        currentExp = currentExp,
    //        currentNextRank = nextRank
    //    };

    //    bf.Serialize(file, data);
    //    file.Close();
    //}

    //public void Load()
    //{
    //    if(File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);

    //        Data data = (Data)bf.Deserialize(file);
    //        file.Close();

    //        highestLevel = data.currentLevel;
    //        scraps = data.currentScraps;
    //        meds = data.currentMeds;
    //        rank = data.currentRank;
    //        currentExp = data.currentExp;
    //        nextRank = data.currentNextRank;
    //    }

    //}


    //public void HealHealth(float heal)
    //{
    //    Health += heal;
    //}

    // hearecho code - BeatHeart, CheckBeat, ScaleBeatRange

    //Beat

//{
//    yield return new WaitForSeconds(heartRate);
//    BeatHeart();
//    //StartCoroutine(HeartBeat());
//}
//void BeatHeart()
//{
//    if (!reduceBeat)
//    {
//        heartBeatRange += beatIncrement;
//        // Debug.Log("BEEP");
//    }
//    else
//    {

//        heartBeatRange -= beatIncrement;
//        // Debug.Log("BOOP");
//    }
//    CheckBeat();
//}

//void CheckBeat()
//{
//    if (heartBeatRange > hMaxRange)
//    {
//        reduceBeat = true;
//    }
//    if (heartBeatRange < 0)
//    {
//        reduceBeat = false;
//        //beatCount++;
//    }

//    ScaleBeatRange();

//}

//void ScaleBeatRange()
//{
//    Vector3 temp = transform.localScale;

//    temp.x = heartBeatRange;
//    temp.y = 0.01f;
//    temp.z = heartBeatRange;

//    heart.transform.localScale = temp;
//}
