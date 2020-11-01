using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public int tutorial;
    public int techDemo;
    int scenenum;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            SceneManager.LoadScene(tutorial);
        }
        if (Input.GetKey(KeyCode.S))
        {
            SceneManager.LoadScene(techDemo);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Quit();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(scenenum);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
