using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager control = null;
    UIManager pager;
    SceneManageMent director;

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
        //playerdeath = false;
        director = GameObject.Find("Scene Manager").GetComponent<SceneManageMent>();
        pager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        pager.ToTitle();
    }

    public void GoToNextLevel()
    {
        //PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        Player player = GameObject.Find("Player").GetComponent<Player>();

        //save player data from level and load to next level
        player.Save();
        director.LoadNextLevel();
        pager.ToGameplay();
    }

    public void GoToMainMenu()
    {
        //PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        Player player = GameObject.Find("Player").GetComponent<Player>();

        player.Save();
        director.LoadMainScene();
        pager.ToMainMenu();
    }

    //public void PlayerDeath() {}

}

