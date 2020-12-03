using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager control = null;
    public int highestLevel;
    public int scraps;
    public int meds;

    private int currentLevel;
    [HideInInspector]
    public int rank;
    [HideInInspector]
    public int currentExp;
    [HideInInspector]
    public int nextRank;

    [HideInInspector]
    public bool playerdeath;



    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerdeath = false;
        nextRank = 10;
    }



    //variables to adjust experience and currency
    public void AddScraps(int collected)
    {
        scraps += collected;
    }
    public void AddMed(int collected)
    {
        meds += collected;
    }
    public void AddExp(int expAquired)
    {
        currentExp += expAquired;
        CheckExp();
    }

    //checks if player ranks up
    private void CheckExp()
    {
        if(currentExp > nextRank)
        {
            rank++;
            nextRank += 5;
        }
    }


    // Save/Load methods
    void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameSave.dat");

        Data data = new Data
        {
            //data being stored
            currentLevel = highestLevel,
            currentScraps = scraps,
            currentMeds = meds,
            currentRank = rank,
            currentExp = currentExp,
            currentNextRank = nextRank
        };

        bf.Serialize(file, data);
        file.Close();
    }

    void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/gameSave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameSave.dat", FileMode.Open);

            Data data = (Data)bf.Deserialize(file);
            file.Close();

            highestLevel = data.currentLevel;
            scraps = data.currentScraps;
            meds = data.currentMeds;
            rank = data.currentRank;
            currentExp = data.currentExp;
            nextRank = data.currentNextRank;
        }

    }


}

//Data
[Serializable]
class Data
{
    public int currentRank;
    public int currentExp;
    public int currentNextRank;

    public int currentLevel;
    public int currentScraps;
    public int currentMeds;
}