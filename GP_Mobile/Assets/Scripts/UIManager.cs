using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager screen = null;
    private SceneManageMent director;
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

    Screens current;

    GameObject previousScreen;
    GameObject currentScreen;

    //used if outside game manager
    private void Awake()
    {
        if(screen == null)
        {
            DontDestroyOnLoad(gameObject);
            screen = this;
        }
        else if(screen != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<SceneManageMent>();

        titleScreen.SetActive(false);
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
        director.LoadTitleScene();
    }

    public void ToMainMenu()
    {
        MakePrevious();
        current = Screens.MainMenu;
        Screenchange();
        director.LoadMainScene();
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

}
