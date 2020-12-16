using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health;
    float maxHealth = 100;

    [HideInInspector]
    public int rank;
    [HideInInspector]
    public int currentExp;
    [HideInInspector]
    public int nextRank;
    [HideInInspector]
    public int scraps;
    [HideInInspector]
    public int meds;
    public Passages passageWay = null;

    // UI elements used to update information for user
    Counter counters;
    //Text levelText;
    //Text ExpText;

    // Start is called before the first frame update
    void Start()
    {
        //Load();
        
        SaveManager.recorder.LoadFile();
        counters = GameObject.Find("Counters").GetComponent<Counter>();

        CheckExp();

        counters.UpdateHealthBar();
        counters.UpdateMedCounter();
        counters.UpdateScrapCounter();
        counters.UpdateRankText();
        counters.UpdateEXPText();

    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        counters.UpdateHealthBar();

        if(health < 0)
        {
            GameManager.control.GoToMainMenu();
        }
    }

    //variables to adjust experience and currency
    public void AddScraps(int collected)
    {
        scraps += collected;
        //counters.UpdateScrapCounter();
    }

    public void AddMed(int collected)
    {
        meds += collected;
        //counters.UpdateMedCounter();
    }

    public void AddExp(int expAquired)
    {
        currentExp += expAquired;
        //CheckExp();
    }

    public void HealPlayer()
    {
        if (health < maxHealth) 
        if(meds >= 10)
        {
            health += 20;
            UseMeds(10);

            if (health > maxHealth) health = maxHealth;

            //counters.UpdateHealthBar();
        }
    }

    public void UseMeds(int medsUsed)
    {
        if(meds >= medsUsed)
        {
            meds -= 10;
            //counters.UpdateMedCounter();
        }
    }

    public bool UseScraps(int scrapsUsed)
    {
        if(scraps >= scrapsUsed)
        {
            scraps -= scrapsUsed;
            //counters.UpdateScrapCounter();
            return true;
        }
        return false;
    }

    //checks if player ranks up
    public void CheckExp()
    {
        if (currentExp >= nextRank)
        {
            rank++;
            nextRank += 5;
            currentExp = 0;
        }

        //counters.UpdateRankText();
        //counters.UpdateEXPText();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Passage"))
        {
            passageWay = other.GetComponent<Passages>();
            passageWay.ShowCost();
        }
    }


}

//unused code
    //private void OnTriggerExit(Collider other)
    //{
    //    if(passageWay != null) passageWay.infobox.text = "";
    //        passageWay = null;
        
    //}


    // Save/Load methods

    //public void Save()
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");

    //    PlayerData data = new PlayerData
    //    {
    //        //data being stored
    //        currentRank = rank,
    //        currentExp = currentExp,
    //        currentNextRank = nextRank,
    //        currentScraps = scraps,
    //        currentMeds = meds
    //    };

    //    bf.Serialize(file, data);
    //    file.Close();

    //    //GlobalSettings.gameData.CreateBackUpPlayerData();
    //}