using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHintText : MonoBehaviour
{
    public float time;

    public void ClearOutText()
    {
        StartCoroutine(EraseText());
    }

    IEnumerator EraseText()
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Text>().text = null;
    }
}
