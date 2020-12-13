using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager screen;

    public GameObject titleScreen;
    public GameObject SaveScreen;
    public GameObject mainMenuScreen;
    public GameObject gamePlayScreen;
    public GameObject pauseScreen;

    enum Screens
    {
        Title,
        Save,
        MainMenu,
        GamePlay,
        Pause
    }

    Screens current;

    GameObject previousScreen;
    GameObject currentScreen;

    //Counter counters;

    //used if outside game manager
    private void Awake()
    {
        if(screen == null)
        {
            DontDestroyOnLoad(gameObject);
            screen = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //director = GameObject.FindGameObjectWithTag("Director").GetComponent<SceneManageMent>();

        titleScreen.SetActive(false);
        SaveScreen.SetActive(false);
        mainMenuScreen.SetActive(false);
        gamePlayScreen.SetActive(false);
        pauseScreen.SetActive(false);

        current = Screens.Title;
        currentScreen = titleScreen;
    }

    public void Screenchange()
    {
        previousScreen.SetActive(false);

        switch (current)
        {
            case Screens.Title:
                currentScreen = titleScreen;
                break;
            case Screens.Save:
                currentScreen = SaveScreen;
                break;
            case Screens.MainMenu:
                currentScreen = mainMenuScreen;
                break;
            case Screens.GamePlay:
                currentScreen = gamePlayScreen;
                break;
            case Screens.Pause:
                currentScreen = pauseScreen;
                break;
        }

        currentScreen.SetActive(true);
    }

    public void MakePrevious()
    {
        previousScreen = currentScreen;
    }

    public void ToTitle()
    {
        MakePrevious();
        current = Screens.Title;
        Screenchange();
    }

    public void ToSave()
    {
        MakePrevious();
        current = Screens.Save;
        Screenchange();
    }

    public void ToMainMenu()
    {
        MakePrevious();
        current = Screens.MainMenu;
        Screenchange();
    }

    public void ToGameplay()
    {
        MakePrevious();
        current = Screens.GamePlay;
        Screenchange();
    }

    public void ToPause()
    {
        MakePrevious();
        Time.timeScale = 0f;
        current = Screens.Pause;
        Screenchange();
    }

    public void ToResume()
    {
        MakePrevious();
        Time.timeScale = 1f;
        current = Screens.GamePlay;
        Screenchange();
    }



    public void Heal()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().HealPlayer();
    }

    public void RemoveBlockage()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();

        GameObject HintDisplay = GameObject.FindGameObjectWithTag("Hint");
        Text gameHints = HintDisplay.GetComponent<Text>();

        //the player is infront of the blocked passage
        if (player.passageWay != null)
        {
            switch (player.UseScraps(player.passageWay.cost))
            {
                case true:
                    player.passageWay.OpenPath();
                    gameHints.text = "You used your scraps to fashion a tool to get open the passage";
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

        HintDisplay.GetComponent<GameHintText>().ClearOutText();

    }

}
