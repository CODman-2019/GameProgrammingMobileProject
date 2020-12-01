using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManageMent : MonoBehaviour
{

    //[HideInInspector]
    //public int TitleScreen = 0;
    //[HideInInspector]
    //public int MainScreen = 1;
    [HideInInspector]
    public int Tutorial = 2;
    [HideInInspector]
    public int Techdemo = 5;
    [HideInInspector]
    public int MainStart = 6;

    [HideInInspector]
    public int Level = 0;

    public static SceneManageMent direction = null;

    private void Awake()
    {
        if(direction == null)
        {
            DontDestroyOnLoad(gameObject);
            direction = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainMenu");
        Level = 0;
    }

    public void LoadTechScene()
    {
        SceneManager.LoadScene(Techdemo);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(Tutorial);
    }

    public void LoadNextLevel()
    {
        Level++;
        SceneManager.LoadScene("Level " + Level);
    }
}
