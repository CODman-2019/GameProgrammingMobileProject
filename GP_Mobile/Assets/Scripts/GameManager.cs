using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager control = null;
    UIManager pager;
    SceneManageMent director;
    Player player = null;
    Text gameHints;

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
        player = GameObject.Find("Player").GetComponent<Player>();
        player.Save();

        //save player data from level and load to next level
        director.LoadNextLevel();
        pager.ToGameplay();

    }

    public void OpenPassage()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        //the player is infront of the blocked passage
        if(player.passageWay != null)
        {
            gameHints = GameObject.FindGameObjectWithTag("Hint").GetComponent<Text>();
                    
            switch (player.UseScraps(player.passageWay.cost))
            {
                case true:
                    gameHints.text = "You used your scraps to fashion a tool to get open the passage";
                    player.passageWay.OpenPath();
                    break;

                case false:
                    gameHints.text = "You don't have enough scrap to use to get through";
                    break;
            }   
        }
        else
        {
            gameHints.text = "there is nothing around you";
        }
    }

    public void HealPlayer()
    {
        GameObject.Find("Player").GetComponent<Player>().HealPlayer();
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

