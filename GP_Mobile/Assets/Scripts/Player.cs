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
        GameManager.control.Load();
        counters = GameObject.Find("Counters").GetComponent<Counter>();

        CheckExp();

        counters.UpdateHealthBar(this);
        counters.UpdateMedCounter(this);
        counters.UpdateScrapCounter(this);
        counters.UpdateRankText(this);
        counters.UpdateEXPText(this);

    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        counters.UpdateHealthBar(this);

        if(health < 0)
        {
            GameManager.control.GoToMainMenu();
        }
    }

    //variables to adjust experience and currency
    public void AddScraps(int collected)
    {
        scraps += collected;
        counters.UpdateScrapCounter(this);
    }

    public void AddMed(int collected)
    {
        meds += collected;
        counters.UpdateMedCounter(this);
    }

    public void AddExp(int expAquired)
    {
        currentExp += expAquired;
        CheckExp();
    }

    public void HealPlayer()
    {
        if (health < maxHealth) 
        if(meds >= 10)
        {
            health += 20;
            UseMeds(10);

            if (health > maxHealth) health = maxHealth;

            counters.UpdateHealthBar(this);
        }
    }

    public void UseMeds(int medsUsed)
    {
        if(meds >= medsUsed)
        {
            meds -= 10;
            counters.UpdateMedCounter(this);
        }
    }

    public bool UseScraps(int scrapsUsed)
    {
        if(scraps >= scrapsUsed)
        {
            scraps -= scrapsUsed;
            counters.UpdateScrapCounter(this);
            return true;
        }
        return false;
    }

    //checks if player ranks up
    private void CheckExp()
    {
        if (currentExp >= nextRank)
        {
            rank++;
            nextRank += 5;
            currentExp = 0;
        }

        counters.UpdateRankText(this);
        counters.UpdateEXPText(this);
        //counters.EXPText.text = currentExp.ToString() + " / " + nextRank.ToString() + " EXP";
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