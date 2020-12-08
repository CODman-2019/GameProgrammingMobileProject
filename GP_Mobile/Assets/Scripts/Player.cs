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
    float maxHealth;

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
    [HideInInspector]
    public Passages passageWay = null;

    // UI elements used to update information for user
    Counter counters;
    //Text levelText;
    //Text ExpText;

    // Start is called before the first frame update
    void Start()
    {
        Load();

        counters = GameObject.Find("Counters").GetComponent<Counter>();

        CheckExp();
        counters.UpdateHealthBar(health);
        counters.UpdateMedCounter(meds);
        counters.UpdateScrapCounter(scraps);
        counters.UpdateRankText(rank);
        counters.UpdateEXPText(this);

        //levelText = GameObject.Find("PlayerLevel").GetComponent<Text>();
        //ExpText = GameObject.Find("EXPText").GetComponent<Text>();
        //levelText.text = "Lv: "+ rank.ToString();
        //ExpText.text = currentExp.ToString() + " / " + nextRank.ToString() + " EXP";
    }

    //public void ResetPlayerHealth()
    //{
    //    health = GlobalSettings.gameData.health;
    //}



    public void TakeDamage(float damage)
    {
        health -= damage;
        counters.UpdateHealthBar(health);

        if(health < 0)
        {
            GameManager.control.GoToMainMenu();
        }
    }

    //variables to adjust experience and currency

    public void AddScraps(int collected)
    {
        scraps += collected;
        counters.UpdateScrapCounter(scraps);
    }
    public void AddMed(int collected)
    {
        meds += collected;
        counters.UpdateMedCounter(meds);
    }
    public void AddExp(int expAquired)
    {
        currentExp += expAquired;
        CheckExp();
    }
    public void HealPlayer()
    {
        if(meds >= 10)
        {
            health += 20;
            UseMeds(10);
            counters.UpdateHealthBar(health);
        }
    }

    public void UseMeds(int medsUsed)
    {
        if(meds >= medsUsed)
        {
            meds -= 10;
            counters.UpdateMedCounter(meds);
        }
    }

    public bool UseScraps(int scrapsUsed)
    {
        if(scraps >= scrapsUsed)
        {
            scraps -= scrapsUsed;
            counters.UpdateScrapCounter(scraps);
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

        counters.UpdateRankText(rank);
        counters.EXPText.text = currentExp.ToString() + " / " + nextRank.ToString() + " EXP";
        //counters.UpdateEXPText(this);
        //levelText.text = "Lv: " + rank.ToString();
        //ExpText.text = currentExp.ToString() + " / " + nextRank.ToString() + " EXP";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Passage"))
        {
            passageWay = other.GetComponent<Passages>();
            passageWay.ShowCost();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(passageWay != null) passageWay.infobox.text = "";
            passageWay = null;
        
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

        //GlobalSettings.gameData.CreateBackUpPlayerData();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

           // GlobalSettings.gameData.CheckPlayerData();
            rank = data.currentRank;
            currentExp = data.currentExp;
            nextRank = data.currentNextRank;
            scraps = data.currentScraps;
            meds = data.currentMeds;
            
        }
        //otherwise SAVE current states
        else
        {
            Save();
        }


    }


}

[Serializable]
class PlayerData
{
    public int currentRank;
    public int currentExp;
    public int currentNextRank;
    public int currentScraps;
    public int currentMeds;
}
