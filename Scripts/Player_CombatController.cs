using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CombatController : MonoBehaviour
{
    public Animator anim;
    public int noOfClicks = 0;
    public PlayerMovement playermovement;
    public float maxComboDelay = 0.9f;
    public float runpunchdelay = 0.9f;
    private float lastclickedtime = 0;
    private bool drawsword;
    private float lastrunpunchtime = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        drawsword = playermovement.drawsword;
        if(Time.time -lastclickedtime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            lastclickedtime = Time.time;
            noOfClicks++;
            if(noOfClicks == 1 && drawsword == true)
            {
                anim.SetBool("SwordAttack1", true);
            }
            else if (noOfClicks == 1 && drawsword == false && Input.GetAxisRaw("Horizontal")!=0 && (Time.time-lastrunpunchtime >runpunchdelay))
            {
                lastrunpunchtime = Time.time;
                anim.SetTrigger("RunFistAttack");
            }
            else if (noOfClicks == 1 && drawsword == false && Input.GetAxisRaw("Horizontal")==0)
            {
                anim.SetBool("FistAttack1", true);
            }
            
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
    }

    public void return1()
    {
        if (drawsword)
        {
            if (noOfClicks >= 2)
            {
                anim.SetBool("SwordAttack2", true);
            }
            else
            {
                anim.SetBool("SwordAttack1", false);
                noOfClicks = 0;
            }
        }
        else
        {
            if (noOfClicks >= 2)
            {
                anim.SetBool("FistAttack2", true);
            }
            else
            {
                anim.SetBool("FistAttack1", false);
                noOfClicks = 0;
            }
        }
    }

    public void return2()
    {
        if (drawsword)
        {
            if (noOfClicks >= 3)
            {
                anim.SetBool("SwordAttack3", true);
            }
            else
            {
                anim.SetBool("SwordAttack2", false);
                noOfClicks = 0;
            }
        }
        else
        {
            if (noOfClicks >= 3)
            {
                anim.SetBool("FistAttack3", true);
            }
            else
            {
                anim.SetBool("FistAttack2", false);
                noOfClicks = 0;
            }
        }
    }

    public void return3()
    {
        if (drawsword)
        {
            anim.SetBool("SwordAttack1", false);
            anim.SetBool("SwordAttack2", false);
            anim.SetBool("SwordAttack3", false);
            noOfClicks = 0;
        }
        else
        {
            anim.SetBool("FistAttack1", false);
            anim.SetBool("FistAttack2", false);
            anim.SetBool("FistAttack3", false);
            noOfClicks = 0;
        }
    }

}

