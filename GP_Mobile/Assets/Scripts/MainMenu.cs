using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : ButtonManager
{
    public int tutorial;
    public int techDemo;
    int scenenum;


    public void Tutorial()
    {
        SceneManager.LoadScene(tutorial);
    }

    public void Demo()
    {
        SceneManager.LoadScene(techDemo);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(scenenum);
    }

}
