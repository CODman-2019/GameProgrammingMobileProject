using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    //Player player;
    public Text scrapsCounter;
    public Text medsCounter;
    public Text RankText;
    public Text EXPText;
    public Slider hpBar;

    // Start is called before the first frame update
    //private void Start()
    //{
    //    player = GameObject.Find("Player").GetComponent<Player>();
    //}

    public void UpdateScrapCounter(int value)
    {
        //scrapsCounter.text = player.scraps.ToString();
        scrapsCounter.text = value.ToString();
    }

    public void UpdateMedCounter(int value)
    {
        //medsCounter.text = player.meds.ToString();
        medsCounter.text = value.ToString();
    }

    public void UpdateHealthBar(float value)
    {
        //check if the value is greater than the max value
        if (value > hpBar.maxValue)
            value = hpBar.maxValue;
        hpBar.value = value;
    }

    public void UpdateRankText(int pRank)
    {
        RankText.text = "Rank: " + pRank.ToString();
    }

    public void UpdateEXPText(Player player)
    {
        EXPText.text = player.currentExp.ToString() + " / " + player.nextRank.ToString() + " EXP";

    }
}
