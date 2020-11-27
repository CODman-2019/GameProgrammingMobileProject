using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    //public int sceneNum;
    public bool ToMainMenu;
    public bool ToNextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //SceneManager.LoadScene(sceneNum);
            if (ToMainMenu)
            {
                GameManager.control.ToMainMenu();
            }
            if (ToNextLevel)
            {
                GameManager.control.GoToNextLevel();
            }
        }
    }
}
