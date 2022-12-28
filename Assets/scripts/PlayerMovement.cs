using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //physics
    [SerializeField] float gravity;

    //Horizontal movement
    private float movementX;
    [SerializeField] private float speed;
    private bool isFacingRight = true;

    //Physics
    [SerializeField] private Rigidbody2D rb;

    //Isgrounded
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    private bool wasGrounded;

    //Jumping
    [SerializeField] private float jumpForce;
    private bool doubleJump;

    //Wall jumping
    [SerializeField] private Transform wallCheck;
    private bool isTouchingWall;
    [SerializeField] private LayerMask wallLayer;
    private bool wallJump;
    private bool wasTouchingWall;

    //Dashing
    // private bool canDash = true;
    // private bool isDashing;
    // private float dashingPower = 24f;
    // private float dashingTime = 0.2f;
    // private float dashingCooldown = 1f;
    // [SerializeField] TrailRenderer tr;

    //Partical Effects

    [SerializeField] ParticleSystem jumpEffect;


    private void Update()
    {
        // if (isDashing)
        // {
        //     return;
        // }

        Flip();

        movementX = 0;

        isTouchingWall = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.1f, 0.3f), 0, wallLayer);
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.4f, .2f), 0, groundLayer);
        
        if (isGrounded)
        {
            wallJump = false;
            if (!Input.GetButton("Jump"))
            {
                doubleJump = false;
            }
        } else 
        {
            movementX = Input.GetAxisRaw("Horizontal");
        }
        
        if (isTouchingWall && !isGrounded)
        {
            doubleJump = false;
            wallJump = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;
        } 
        else
        {
            rb.gravityScale = gravity;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = !doubleJump;
            } else if (wallJump && !isTouchingWall)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                wallJump = !wallJump;
                doubleJump = !doubleJump;
                jumpParticles();
            } else if (doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpParticles();
                doubleJump = !doubleJump;
            }

        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        // {
        //     StartCoroutine(Dash());
        // }
    }
    
    private void FixedUpdate()
    {
        // if (isDashing)
        // {
        //     return;
        // }
        rb.velocity = new Vector2(movementX * speed, rb.velocity.y);
    }




    private void Flip()
    {
        if (isFacingRight && movementX < 0f || !isFacingRight && movementX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // private IEnumerator Dash()
    // {
    //     canDash = false;
    //     isDashing = true;
    //     float originalGravity = rb.gravityScale;
    //     rb.gravityScale = 0f;
    //     rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
    //     tr.emitting = true;
    //     yield return new WaitForSeconds(dashingTime);
    //     tr.emitting = false;
    //     rb.gravityScale = originalGravity;
    //     isDashing = false;
    //     yield return new WaitForSeconds(dashingCooldown);
    //     canDash = true;
    // }

    private void jumpParticles()
    {
        jumpEffect.gameObject.SetActive(true);
        jumpEffect.Stop();
        jumpEffect.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        jumpEffect.Play();
    }
}

