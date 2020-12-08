using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManageMent : MonoBehaviour
{

    [HideInInspector]
    int TitleScreen = 0;
    [HideInInspector]
    int MainScreen = 1;
    [HideInInspector]
    int Tutorial = 2;
    [HideInInspector]
    int Techdemo = 5;
    [HideInInspector]
    int MainStart = 6;

    [HideInInspector]
    public int Level = 0;
    Player player;
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

    //remove this
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main Menu");
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
        Level += 1;
        SceneManager.LoadScene("Level " + Level );
    }
}
