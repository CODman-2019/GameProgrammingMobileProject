using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplies : MonoBehaviour
{
    public float speed;
    bool isMeds;
    bool isScraps;

    public int value;
    public int expBonus;

    private void Update()
    {
        transform.Rotate(speed * 2 * Time.deltaTime, speed * 2 * Time.deltaTime, speed * 2 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerinventory = other.GetComponent<Player>();
            
            if (isMeds)
            {
                playerinventory.AddMed(value);
                playerinventory.AddExp(expBonus);
            }
            if (isScraps)
            {
                playerinventory.AddScraps(value);
                playerinventory.AddExp(expBonus);

            }
        }
    }

}
