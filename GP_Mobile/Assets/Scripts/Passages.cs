using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passages : MonoBehaviour
{
    public int cost;
    [HideInInspector]
    public Text infobox;

    //public bool isSwitch;
    //public bool isPassage;

    private void Start()
    {
        infobox = GameObject.FindGameObjectWithTag("Hint").GetComponent<Text>();
    }

    public void ShowCost()
    {
        infobox.text = "This will cost " + cost +" scraps";
    }

    public void OpenPath()
    {
        gameObject.transform.position = new Vector3(0, -2.5f, 0);
    }
}
