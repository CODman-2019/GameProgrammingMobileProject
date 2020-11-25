using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager buttonControl;
    //public int currentLevel;

    void Awake()
    {
        if(buttonControl == null)
        {
            buttonControl = this;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ReturnToTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void ReturntoMenuScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
