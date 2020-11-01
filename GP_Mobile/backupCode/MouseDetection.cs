using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetection : MonoBehaviour
{
    public GameObject player;
    float access;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 temp = transform.localScale;
        access = player.GetComponent<PlayerController>().distance * 2;

        temp.x = access;
        temp.z = access;

        transform.localScale = temp;
        //transform.localScale.Set(access, 0, access);

    }
}
