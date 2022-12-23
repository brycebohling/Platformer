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

    //Jumping
    [SerializeField] private float jumpForce;
    private bool doubleJump;

    //Wall jumping
    [SerializeField] private Transform wallCheck;
    private bool isWallTouch;
    private bool isSliding;
    [SerializeField] float wallSlidingSpeed;
    [SerializeField] private LayerMask wallLayer;
    private bool wallJump;

    //Dashing
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    [SerializeField] TrailRenderer tr;


    private void Update()
    {
        Flip();

        movementX = 0;

        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.2f, 1f), 0, wallLayer);
        
        if (isGrounded()) 
        {
            if (!Input.GetButton("Jump"))
            {
                doubleJump = false;
                wallJump = false;
            }
        } else 
        {
            movementX = Input.GetAxisRaw("Horizontal");
        }
        
        if (isWallTouch && !isGrounded())
        {
            isSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;
        } else
        {
            isSliding = false;
            rb.gravityScale = gravity;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = !doubleJump;
            } 
            else if (isSliding)
            {
                wallJump = true;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(movementX * speed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(0.4f, .2f), 0, groundLayer);
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

