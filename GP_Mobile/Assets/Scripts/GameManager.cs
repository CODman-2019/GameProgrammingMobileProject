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

    public GameObject titleScreen;
    public GameObject mainMenuScreen;
    public GameObject gamePlayScreen;
    public GameObject pauseScreen;

    enum Screens
    {
        Title,
        MainMenu,
        GamePlay,
        Pause
    }

    Screens screen;

    [HideInInspector]
    public int TitleScreen = 0;
    [HideInInspector]
    public int MainScreen = 1;
    [HideInInspector]
    public int Tutorial = 2;
    [HideInInspector]
    public int Techdemo = 5;
    [HideInInspector]
    public int MainStart = 6;

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
        currentLevel = MainStart;

        screen = Screens.Title;
        ScreenChange();
    }

    //method used to enable and disable other UI elements
    public void ScreenChange()
    {
        switch (screen)
        {
            case Screens.Title:
                titleScreen.SetActive(true);
                pauseScreen.SetActive(false);
                gamePlayScreen.SetActive(false);
                mainMenuScreen.SetActive(false);
                SceneManager.LoadScene(TitleScreen);
                break;
            case Screens.MainMenu:
                titleScreen.SetActive(false);
                pauseScreen.SetActive(false);
                gamePlayScreen.SetActive(false);
                mainMenuScreen.SetActive(true);
                SceneManager.LoadScene(MainScreen);
                break;
            case Screens.GamePlay:
                titleScreen.SetActive(false);
                pauseScreen.SetActive(false);
                gamePlayScreen.SetActive(true);
                mainMenuScreen.SetActive(false);
                break;
            case Screens.Pause:
                titleScreen.SetActive(false);
                pauseScreen.SetActive(true);
                gamePlayScreen.SetActive(false);
                mainMenuScreen.SetActive(false);
                break;

        }
    }

    //methods for changing scenes and quiting application
    public void QuitGame()
    {
        Application.Quit();
    }

    //methods for changeing UI states/screens + scenes
    public void StartGame()
    {
        OnGamePlay();
        SceneManager.LoadScene(MainStart);
    }

    public void ToTutorial()
    {
        OnGamePlay();
        SceneManager.LoadScene(Tutorial);
    }

    public void GoToNextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }

    public void ToDemo()
    {
        OnGamePlay();
        SceneManager.LoadScene(Techdemo);
    }

    public void ToTitle()
    {
        Time.timeScale = 1f;
        screen = Screens.Title;
        ScreenChange();
        Save();
    }


    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        screen = Screens.MainMenu;
        ScreenChange();
        Load();
    }


    public void OnGamePlay()
    {
        Time.timeScale = 1f;
        screen = Screens.GamePlay;
        ScreenChange();
    }

    public void OnPause()
    {
        screen = Screens.Pause;
        ScreenChange();
        Time.timeScale = 0f;
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