using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float speed;
    bool pickedUp;

    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime ,speed * 2* Time.deltaTime, speed* Time.deltaTime);

    }

    public bool PickedUp()
    {
        return pickedUp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().AddExp(5);

            this.enabled = false;
            this.GetComponent<MeshRenderer>().enabled = false;
            pickedUp = true;
        }
    }

}
