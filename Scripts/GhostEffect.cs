using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    public float ghosteffectdelay1;
    public float ghosteffectdelay2;
    public float ghosteffectdelay3;
    public Soul_Controller soulcontroller;
    public GameObject[] ghosteffects;
    public bool ghosting;
    public Color colour;
    private float timer;
    private float ghosteffectdelay;
    private int soulstage;
    void Start()
    {
        timer = ghosteffectdelay;
    }

    // Update is called once per frame
    void Update()
    {
        soulstage = soulcontroller.soulstage;

        if (soulstage == 1)
            ghosteffectdelay = ghosteffectdelay1;
        else if(soulstage == 2)
            ghosteffectdelay = ghosteffectdelay2;
        else if(soulstage == 3)
            ghosteffectdelay = ghosteffectdelay3;

        if (ghosting)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                GameObject currentghost = Instantiate(ghosteffects[soulstage-1], transform.position, transform.rotation);
                timer = ghosteffectdelay;
                this.GetComponent<SpriteRenderer>().color = colour;
                Sprite currentsprite = GetComponent<SpriteRenderer>().sprite;
                currentghost.transform.localScale = this.transform.localScale;
                currentghost.GetComponent<SpriteRenderer>().sprite = currentsprite;
                Destroy(currentghost, 1f);
            }
        }
    }
}
