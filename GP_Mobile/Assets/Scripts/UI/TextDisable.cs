using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisable : MonoBehaviour
{
    public GameObject textObject;

    private void OnTriggerEnter(Collider other)
    {
        textObject.SetActive(false);
    }
}
