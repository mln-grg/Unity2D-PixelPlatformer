using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 50f)] [SerializeField] private float movementspeed = 40;
    [Range(0, .3f)] [SerializeField] private float movement_smoothening = .05f;
    [Range(0, 20f)] [SerializeField] private float crouchspeed = 20;

    public GhostEffect ghost;
    public LayerMask whatisground;
    public Transform wallcheck;
    public Transform groundcheck;
    public Collider2D crouch_disable_collider;
    public Vector2 walljumpdirection;
    public float jumpforce = 16;
    public float wallslidingspeed;
    public float walljumpforce;
    public float groundcheckradius;
    public float wallcheckradius;
    public int jumplimit;  
    public float jumpdelay;
    public float jumptime;
    

    private Rigidbody2D rb2d;
    private Animator animator;

    private bool isfacingright = true;
    private bool sheathe = false;
    private bool isCrouching;
    private float horizontalinput;
    private bool isjumping;
    private float jumptimecounter;
    private int jumpcount;
    public bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private float canjump;
    private float horizontal_movement;
    private Vector2 refvelocity = Vector2.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        if (isCrouching)
            speed = crouchspeed;
        else
            speed = movementspeed;
        horizontalinput = Input.GetAxisRaw("Horizontal");
        horizontal_movement = horizontalinput * speed;

        

        //GhostEffect
        if (horizontalinput!=0)
        {
            ghost.ghosting = true;
        }
        else
        {
            ghost.ghosting = false;
        }
       
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || isTouchingWall ) )
        {
            Jump();
        }
        
        if (Input.GetKey(KeyCode.Space) && isjumping == true && !isGrounded)
        {
            VariableJump();         
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isjumping = false;
        }

        if (Input.GetKeyDown("c"))
        {
            isCrouching = true;
            crouch_disable_collider.enabled = false;
        }
        if (Input.GetKeyUp("c"))
        {
            isCrouching = false;
            crouch_disable_collider.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            sheathe = !sheathe;
        }

        
        CheckSurroundings();
        CheckIfWallSliding();
        CheckIfCanJump();
        Animations();
    }

    private void FixedUpdate()
    {
        Move(horizontal_movement * Time.fixedDeltaTime);

    }
    private void Move(float move)
    {
        //Horizontal Movement
        Vector3 targetVelocity = new Vector2(move * 10f, rb2d.velocity.y);
        rb2d.velocity = Vector2.SmoothDamp(rb2d.velocity, targetVelocity, ref refvelocity, movement_smoothening);
  
        if (move < 0 && isfacingright)
            Flip();
        if (move > 0 && !isfacingright)
            Flip();

        //wallsilding
        if (isWallSliding && rb2d.velocity.y < -wallslidingspeed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -wallslidingspeed);
        }
     
    }

    private void Jump() 
    {
        
        if (jumpcount <= jumplimit && (Time.time > canjump))
        {
            if (isWallSliding || isTouchingWall)
            {
                isWallSliding = false;
                jumpcount++;
                Vector2 forcetoadd = new Vector2(walljumpforce * walljumpdirection.x, walljumpforce * walljumpdirection.y);
                canjump = Time.time + jumpdelay;
                rb2d.AddForce(forcetoadd, ForceMode2D.Impulse);
            }
            else
            {

                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce);
                //rb2d.velocity = Vector2.up * jumpforce;
        

            }
        }
        
    }
    private void VariableJump()
    {
        if (jumptimecounter > 0)
        {

            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce);
            //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce);
            jumptimecounter -= Time.deltaTime;
        }
        else
        {
            isjumping = false;
        }
    }
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, whatisground);
        isTouchingWall = Physics2D.OverlapCircle(wallcheck.position, wallcheckradius, whatisground);
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb2d.velocity.y <= 0)
        {
            jumpcount = 0;
            isjumping = true;
            jumptimecounter = jumptime;
        }
        
    }

    private void CheckIfWallSliding()
    {
        if(isTouchingWall && isGrounded == false && rb2d.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void Flip()
    {
        isfacingright = !isfacingright;
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
    }

    private void Animations()
    {
        animator.SetFloat("runspeed", Mathf.Abs(horizontal_movement));
        animator.SetBool("IsJumping", !isGrounded);
        animator.SetFloat("VerticalVelocity", rb2d.velocity.y);
        animator.SetBool("IsCrouching", isCrouching);
        animator.SetBool("WallSliding", isWallSliding);
        animator.SetBool("drawSword", sheathe);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(groundcheck.position, groundcheckradius);
        //Gizmos.DrawWireSphere(wallcheck.position, wallcheckradius);
    }
}
