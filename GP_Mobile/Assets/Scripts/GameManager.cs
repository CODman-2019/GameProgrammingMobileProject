using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager control;
    AudioManager musician;
    UIManager pager;
    SceneManageMent director;
    //Player player;
    Text gameHints;


    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //get the managers and set the scene and ui to the title 
        musician = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        director = GameObject.Find("Scene Manager").GetComponent<SceneManageMent>();
        pager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        pager.ToTitle();
        musician.PlayMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //when ever the game moves to the next level
    public void GoToNextLevel()
    {
        //save player data from level and load to next level
        // Save();
        director.LoadNextLevel();
        pager.ToGameplay();
        //Load();
        //musician.PlayGameplay();

    }


    public void GoToTitle()
    {
        director.LoadTitleScene();
        pager.ToTitle();
        musician.PlayMenu();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        //fPlayer player = GameObject.Find("Player").GetComponent<Player>();

        //Save();
        director.LoadMainScene();
        pager.ToMainMenu();
        musician.PlayMenu();
    }



    //public void PlayerDeath() {}
    //public void Save()
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");

    //    Player player = GameObject.Find("Player").GetComponent<Player>();

    //    PlayerSaveData data = new PlayerSaveData
    //    {
    //        //data being stored
    //        currentHealth = player.health,
    //        currentRank = player.rank,
    //        currentExp = player.currentExp,
    //        currentNextRank = player.nextRank,
    //        currentScraps = player.scraps,
    //        currentMeds = player.meds
    //    };

    //    bf.Serialize(file, data);
    //    file.Close();

    //    //GlobalSettings.gameData.CreateBackUpPlayerData();
    //}
    //public void Load()
    //{
    //    if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);

    //        Player player = GameObject.Find("Player").GetComponent<Player>();

    //        PlayerSaveData data = (PlayerSaveData)bf.Deserialize(file);
    //        file.Close();

    //        // GlobalSettings.gameData.CheckPlayerData();
    //        player.health = data.currentHealth;
    //        player.rank = data.currentRank;
    //        player.currentExp = data.currentExp;
    //        player.nextRank = data.currentNextRank;
    //        player.scraps = data.currentScraps;
    //        player.meds = data.currentMeds;
            
    //    }
    //    //otherwise SAVE current states
    //    else
    //    {
    //        Save();
    //    }
    //}

    //public void ErasePlayerData()
    //{
    //    //player.Erase();
    //    if(File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
    //        File.Delete(Application.persistentDataPath + "/PlayerData.dat");
    //}
}

//[Serializable]
//class PlayerSaveData
//{
//    public float currentHealth;
//    public int currentRank;
//    public int currentExp;
//    public int currentNextRank;
//    public int currentScraps;
//    public int currentMeds;
//}


    //public void OpenPassage()
    //{
    //    Player player = GameObject.Find("Player").GetComponent<Player>();

    //    //the player is infront of the blocked passage
    //    if(player.passageWay != null)
    //    {
    //        gameHints = GameObject.FindGameObjectWithTag("Hint").GetComponent<Text>();
                    
    //        switch (player.UseScraps(player.passageWay.cost))
    //        {
    //            case true:
    //                player.passageWay.OpenPath();
    //                gameHints.text = "You used your scraps to fashion a tool to get open the passage";
    //                break;

    //            case false:
    //                gameHints.text = "You don't have enough scrap to use to get through";
    //                break;
    //        }   
    //    }
    //    else
    //    {
    //        gameHints.text = "there is nothing around you";
    //    }
    //}

    //public void HealPlayer()
    //{
    //    GameObject.Find("Player").GetComponent<Player>().HealPlayer();
    //}