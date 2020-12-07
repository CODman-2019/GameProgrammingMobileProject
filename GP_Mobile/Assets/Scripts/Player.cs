using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Player : GlobalSettings
{
    public float Health;
    
    [HideInInspector]
    public int rank;
    [HideInInspector]
    public int currentExp;
    [HideInInspector]
    public int nextRank;
    [HideInInspector]
    int scraps;
    [HideInInspector]
    int meds;

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if(Health < 0)
        {
            GameManager.control.GoToMainMenu();
            //SceneManageMent.direction.LoadMainScene();
        }
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
        if (currentExp > nextRank)
        {
            rank++;
            nextRank += 5;
        }
    }

    // Save/Load methods
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");

        PlayerData data = new PlayerData
        {
            //data being stored
            currentRank = rank,
            currentExp = currentExp,
            currentNextRank = nextRank,
            currentScraps = scraps,
            currentMeds = meds
        };

        bf.Serialize(file, data);
        file.Close();

        GlobalSettings.gameData.CreateBackUpPlayerData();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            GlobalSettings.gameData.CheckPlayerData();
            rank = data.currentRank;
            currentExp = data.currentExp;
            nextRank = data.currentNextRank;
            scraps = data.currentScraps;
            meds = data.currentMeds;
            
        }


    }


}

