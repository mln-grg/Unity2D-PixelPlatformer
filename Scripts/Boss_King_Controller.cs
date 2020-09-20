using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Boss_King_Controller : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    public bool playerRight=true;
    public bool facingRight = true;
    Animator anim;


    public int maxHealth = 100;
    [SerializeField] float currentHealth;

    
    [Header("Player Check")]
    [Space]
    public LayerMask playerMask;
    public int checkRadius;
    public Transform checkOffset;
    [SerializeField] Collider2D playerCollider;


    [Space]
    [Header("Movement")]
    [Space]
    private Vector2 moveDirection;
    private Vector3 playerPosition;
    public float moveForce;
    public float attackRange;


    [Space]
    [Header("Attacking")]
    [Space]
    public float baseAttack = 20;
    public Vector3 attackOffset;
    public float attckRange = 1f;
    public float timer = 0.0f;
    public float cooldownTime = 1.0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PlayerCheck();
        Flip();
    }


    void FixedUpdate()
    {
        rb.AddForce(moveDirection * moveForce);
    }    
    
    private void Move()
    {
        moveDirection = Vector2.zero;

        if (playerCollider!=null)
        {
            Vector3 direction = playerPosition - this.transform.position;
            direction.y = 0;
            anim.SetBool("Idle", false);

            if (direction.magnitude > attackRange)
            {
                moveDirection = direction.normalized;
                anim.SetBool("Run", true);
            }
            else
            {
                moveDirection = Vector2.zero;
                anim.SetBool("Run", false);
                anim.SetBool("Idle", true);
                CheckAttack();
            }
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Run", false);
        }
    }


    private void CheckAttack()
    {

        if (timer > cooldownTime)
        {
                anim.SetTrigger("Attack");
                timer = 0;
        }

        if (timer < cooldownTime + 1)
        { 
            timer += Time.deltaTime; 
        }
    
    }


    private void Flip()
    {
        if (facingRight != playerRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingRight = playerRight;
        }
    }


    void PlayerCheck()
    {
        playerCollider = Physics2D.OverlapCircle(checkOffset.position, checkRadius, playerMask);
        if (playerCollider != null)
        {
            playerPosition = playerCollider.transform.position;
            if (transform.position.x - playerPosition.x < 0)
            {
                playerRight = true;
            }
            else
            {
                playerRight = false;
            }
        }
    }  


    public void Attacking()
    {
        Debug.Log("Attack");
     // I don't know how you're managing the player damage. 
     //SO just modify this when u add that function in it.


     //playerCollider.GetComponent<CharacterController2D>().TakeDamage(baseAttack);
    
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            anim.SetTrigger("Dead");
        }
        else
        {
            anim.SetTrigger("Hit");
        }

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkOffset.position, checkRadius);
    }
}
