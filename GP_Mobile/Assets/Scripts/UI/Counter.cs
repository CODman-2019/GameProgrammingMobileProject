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

    private void FixedUpdate()
    {
        UpdateScrapCounter();
        UpdateEXPText();
        UpdateHealthBar();
        UpdateMedCounter();
        UpdateRankText();
    }

    public void UpdateScrapCounter()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //scrapsCounter.text = player.scraps.ToString();
        scrapsCounter.text = player.scraps.ToString();
    }

    public void UpdateMedCounter()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //medsCounter.text = player.meds.ToString();
        medsCounter.text = player.meds.ToString();
    }

    public void UpdateHealthBar()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        hpBar.value = player.health;
    }

    public void UpdateRankText()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        RankText.text = "Rank: " + player.rank.ToString();
    }

    public void UpdateEXPText()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.CheckExp();
        EXPText.text = player.currentExp.ToString() + " / " + player.nextRank.ToString() + " EXP";
    }
}
