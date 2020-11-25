using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager control;
    public int level;
    public int scraps;
    public int meds;

    public int rank;
    public int currentExp;
    public int nextRank;

    public bool playerdeath;

    public static int titleScreen = 0;
    public static int mainScreen = 1;

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
        level = SceneManager.GetActiveScene().buildIndex;
        playerdeath = false;
    }

    public void ReturnToHub()
    {
        SceneManager.LoadScene(1);
    }

    void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameSave.dat");

        Data data = new Data();
        //data being stored
        data.currentLevel = level;
        data.currentScraps = scraps;
        data.currentMeds = meds;
        data.currentRank = rank;
        data.currentExp = currentExp;
        data.currentNextRank = nextRank;

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

            level = data.currentLevel;
            scraps = data.currentScraps;
            meds = data.currentMeds;
            rank = data.currentRank;
            currentExp = data.currentExp;
            nextRank = data.currentNextRank;
        }

    }


}

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