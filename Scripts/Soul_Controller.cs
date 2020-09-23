using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul_Controller : MonoBehaviour
{
    public float speedmult;
    public float soulstagetimelimit;

    private int soulcount;
    //private bool shouldghost;
    private bool canfight;
    private bool speedincrease;   
    private bool canusebow;
    private bool shadowmode = false;
    public int soulstage = 3;
    private float soulstagetimer;
    void Start()
    {
        soulstagetimer = soulstagetimelimit;
        SoulStage();
    }

    // Update is called once per frame
    void Update()
    {
        soulstagetimer -= Time.deltaTime;
        SoulController();

        if (Input.GetKeyDown("x") && soulstage ==3)
        {
            shadowmode = !shadowmode;
        }
        if (soulstage < 3)
            shadowmode = false;
    }

    private void SoulController()
    {
        while (soulcount>0)
        {
            if (soulstage > 0)
            {
                if (soulstagetimer <= 0)
                {
                    soulstage--;
                    soulstagetimer = soulstagetimelimit;
                    SoulStage();
                }
            }
            else
            {
                if (soulcount > 0)
                {
                    soulcount--;
                    soulstage = 3;
                    SoulStage();
                }
                else
                {
                    //gameover
                }
            }

        }
    }
    private void SoulStage()
    {
        switch (soulstage)
        {
            case 1:
                {
                    canfight = true;
                    canusebow = true;
                    //shouldghost = false;
                    speedincrease = false;
                    break;
                }
            case 2:
                {
                    canusebow = false;
                    canfight = true;
                    //shouldghost = false;
                    speedincrease = false;
                    break;
                }
            case 3:
                {
                    canusebow = false;
                    canfight = false;
                    //shouldghost = true;
                    speedincrease = true;
                    break;
                }
        }

    }
}
