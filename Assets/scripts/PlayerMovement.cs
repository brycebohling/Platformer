using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Horizontal movement
    private float movementX;
    [SerializeField] private float speed;

    private bool isFacingRight = true;

    //Physics
    [SerializeField] private Rigidbody2D rb;

    //Isgrounded
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    //Jumping over ledge
    //[SerializeField] private float hangTime = 0.2f;
    //private float hangCounter;

    //Jumping
    [SerializeField] private float jumpForce;
    private bool doubleJump;

    //Wall jumping
    [SerializeField] private Transform wallCheck;
    private bool isWallTouch;
    private bool isSlideing;
    [SerializeField] float wallSlidingSpeed;
    [SerializeField] private LayerMask wallLayer;

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

        
        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.2f, 1f), 0, wallLayer);
        //if (isGrounded())
        //{
        //    hangCounter = hangTime;
        //}
        //else
        //{
        //    hangCounter -= Time.deltaTime;
        //}
        
        if (isGrounded()) 
        {
            movementX = 0f;
            if (!Input.GetButton("Jump"))
            {
                doubleJump = false;
            }
        } else 
        {
            movementX = Input.GetAxisRaw("Horizontal");
        }
        
        if (isWallTouch && !isGrounded() && movementX != 0)
        {
            isSlideing = true;
        } else
        {
            isSlideing = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = !doubleJump;
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

        if (isSlideing)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    private bool isGrounded()
    {
        // return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(1.7f, 0.24f), 0, groundLayer);
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

