using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings gameData = null;
    BinaryFormatter bf = new BinaryFormatter();

    

    public void Awake()
    {
        if(gameData == null)
        {
            gameData = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateBackUpPlayerData()
    {
        //check if back up is not created
        if (!File.Exists(Application.persistentDataPath + "BU_PlayerData.dat"))
        {
            //ope the player file and create a backup File
            FileStream file = File.Open(Application.persistentDataPath + "PlayerData.dat", FileMode.Open);
            FileStream backup = File.Create(Application.persistentDataPath + "BU_PlayerData.dat");

            //deserialize the player file and close file
            PlayerData pData = (PlayerData)bf.Deserialize(file);
            file.Close();

            //create backup file and copy player data
            BackUpPlayerData playerData = new BackUpPlayerData
            {
                //copy player data to backup
                player_CurrentScraps = pData.currentScraps,
                player_CurrentExp = pData.currentExp,
                player_CurrentMeds = pData.currentMeds,
                player_CurrentNextRank = pData.currentNextRank,
                player_CurrentRank = pData.currentRank
            };
            //serialize and close file
            bf.Serialize(backup, playerData);
            backup.Close();
        }
        //otherwise check player data
        else
        {
            CopyPlayerData();
        }
    }

    public void CopyPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "BU_PlayerData.dat"))
        {
            //ope the player file and create a backup File
            FileStream file = File.Open(Application.persistentDataPath + "PlayerData.dat", FileMode.Open);
            FileStream backup = File.Open(Application.persistentDataPath + "BU_PlayerData.dat", FileMode.Open);

            //deserialize the player file and close file
            PlayerData pData = (PlayerData)bf.Deserialize(file);
            file.Close();

            //create backup file and copy player data
            BackUpPlayerData playerData = new BackUpPlayerData
            {
                //copy player data to backup
                player_CurrentScraps = pData.currentScraps,
                player_CurrentExp = pData.currentExp,
                player_CurrentMeds = pData.currentMeds,
                player_CurrentNextRank = pData.currentNextRank,
                player_CurrentRank = pData.currentRank
            };
            //serialize and close file
            bf.Serialize(backup, playerData);
            backup.Close();
        }
    }

    public void CheckPlayerData()
    {   //checking if playerdata exists
        if(File.Exists(Application.persistentDataPath + "PlayerData.dat"))
        {
            //checks if back up data exists
            if(File.Exists(Application.persistentDataPath + "BU_PlayerData.dat"))
            {
                FileStream playerD = File.Open(Application.persistentDataPath + "PlayerData.dat", FileMode.Open);
                FileStream backupD = File.Open(Application.persistentDataPath + "BU_PlayerData.dat", FileMode.Open);

                PlayerData pData = (PlayerData)bf.Deserialize(playerD);
                BackUpPlayerData buData = (BackUpPlayerData)bf.Deserialize(backupD);
                backupD.Close();

                //if any of the player data is not the same as the backUp data copy the back up data to the player data
                if (pData.currentRank != buData.player_CurrentRank)
                    pData.currentRank = buData.player_CurrentRank;
                if (pData.currentExp != buData.player_CurrentExp)
                    pData.currentExp = buData.player_CurrentExp;
                if (pData.currentNextRank != buData.player_CurrentNextRank)
                    pData.currentNextRank = buData.player_CurrentNextRank;
                if (pData.currentScraps != buData.player_CurrentScraps)
                    pData.currentScraps = buData.player_CurrentScraps;
                if (pData.currentMeds != buData.player_CurrentMeds)
                    pData.currentMeds = buData.player_CurrentMeds;

                //serialize files and close filestream
                bf.Serialize(playerD, pData);
                playerD.Close();

            }

            else
            {
                //Debug.LogError("Backup data not Available - creating new back up data");
                CreateBackUpPlayerData();
            }
        }
        else
        {
            Debug.LogError("Player data not Available - creating new player data");
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.Save();
        }
    }




}
//Data
[Serializable]
class PlayerData
{
    public int currentRank;
    public int currentExp;
    public int currentNextRank;
    public int currentScraps;
    public int currentMeds;
}

[Serializable]
class BackUpPlayerData
{
    public int player_CurrentRank;
    public int player_CurrentExp;
    public int player_CurrentNextRank;
    public int player_CurrentScraps;
    public int player_CurrentMeds;
}