using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    //variables used for destination
    public bool ToMainMenu;
    public bool ToNextLevel;

    private void OnTriggerEnter(Collider other)
    {
        //if the player hits the trigger
        if (other.CompareTag("Player"))
        {
            //if it is set for the main menu have the game manager load the menu screen
            if (ToMainMenu)
            {
                GameManager.control.ToMainMenu();
            }
            //if it is set for the next level, load the next scene.
            if (ToNextLevel)
            {
                GameManager.control.GoToNextLevel();
            }
        }
    }
}
