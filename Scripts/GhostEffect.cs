using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    public float ghosteffectdelay;
    public GameObject ghosteffect;
    public bool ghosting;
    public Color colour;
    private float timer;
    void Start()
    {
        timer = ghosteffectdelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (ghosting)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                GameObject currentghost = Instantiate(ghosteffect, transform.position, transform.rotation);
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
