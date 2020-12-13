using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    //things needed
    /// 
    /// - multiple saves
    /// - save and load data
    /// - delete data
    /// -- optional copy data
    /// 
    public static SaveManager recorder;

    public Text numText;
    public Text fileInfoText;

    int maxFiles = 5;
    int fileNum = 1;

    [HideInInspector]
    int saveFile;

    string infoData;

    

    private void Awake()
    {
        if(recorder == null)
        {
            recorder = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        numText.text = fileNum.ToString();
        GetFileData();
    }

    public void NextFile()
    {
        fileNum++;
        if (fileNum > maxFiles) fileNum = 1;
        numText.text = fileNum.ToString();

        GetFileData();
    }

    public void PreviousFile()
    {
        fileNum--;
        if (fileNum < 1) fileNum = maxFiles;
        numText.text = fileNum.ToString();

        GetFileData();
    }

    public void GetFileData()
    {
        infoData = null;

        if (File.Exists(Application.persistentDataPath + "/PlayerSave" + fileNum + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream currentFile = File.Open(Application.persistentDataPath + "/PlayerSave" + fileNum + ".dat", FileMode.Open);

            PlayerSaveData dataInfo = (PlayerSaveData)bf.Deserialize(currentFile);

            currentFile.Close();
            
            infoData = "Health: " + dataInfo.currentHealth + "    Rank: " + dataInfo.currentRank;
            infoData += "    Scraps: "+ dataInfo.currentScraps + "    Meds: "+ dataInfo.currentMeds ;
        }
        else
        {
            infoData = "This file has not been created yet";
        }

            fileInfoText.text = infoData;
    }

    public void StoreSaveFile()
    {
        saveFile = fileNum;
        //FileStream file = File.Create(Application.persistentDataPath + "/PlayerSave" + saveFile + ".dat");

    }

    public void SaveFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerSave"+saveFile +".dat");

        Player player = GameObject.Find("Player").GetComponent<Player>();

        PlayerSaveData data = new PlayerSaveData
        {
            //data being stored
            currentHealth = player.health,
            currentRank = player.rank,
            currentExp = player.currentExp,
            currentNextRank = player.nextRank,
            currentScraps = player.scraps,
            currentMeds = player.meds
        };

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadFile()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerSave" + saveFile + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerSave" + saveFile + ".dat", FileMode.Open);

            Player player = GameObject.Find("Player").GetComponent<Player>();

            PlayerSaveData data = (PlayerSaveData)bf.Deserialize(file);
            file.Close();

            // GlobalSettings.gameData.CheckPlayerData();
            player.health = data.currentHealth;
            player.rank = data.currentRank;
            player.currentExp = data.currentExp;
            player.nextRank = data.currentNextRank;
            player.scraps = data.currentScraps;
            player.meds = data.currentMeds;

        }
        //otherwise SAVE current states
        else
        {
            SaveFile();
        }
    }



    public void EraseFile()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerSave"+saveFile+".dat"))
            File.Delete(Application.persistentDataPath + "/PlayerSave" + saveFile + ".dat");
    }
}

[Serializable]
class PlayerSaveData
{
    public float currentHealth;
    public int currentRank;
    public int currentExp;
    public int currentNextRank;
    public int currentScraps;
    public int currentMeds;
}

/// unused methods
/// 
///     //public void CopySaveFile()
