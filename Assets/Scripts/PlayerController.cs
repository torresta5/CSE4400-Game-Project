using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float jumpTime = 0.5f;

    [HideInInspector] public bool isFacingLeft;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private float directionX;
    private SpriteRenderer sprite;
    private bool isJumping;
    private bool isFalling;
    private float jumpTimeCounter;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 3f;
    private bool isFacingRight = true;


    public int jumpSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(isDashing)
        {
            return;
        }

        directionX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(directionX * 7f, rb.velocity.y);

        #region Jumping

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.gravityScale = 5;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

        }

        if (Input.GetButton("Jump"))
        {
            if (jumpTimeCounter > 0 && isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumpTimeCounter -= Time.deltaTime;
            }
            else if (jumpTimeCounter == 0)
            {
                isFalling = true;
                isJumping = false;
            }
            else
            {
                isJumping = false;
            }

        }

        if (Input.GetButtonUp("Jump"))
        {
            isFalling = true;
            isJumping = false;
        }

        if (isFalling)
        {

            if (rb.gravityScale < 6)
            {
                rb.gravityScale += Time.deltaTime;
                if (IsGrounded())
                {
                    rb.gravityScale = 5;
                    isFalling = false;
                }
            }
        }

        if (!IsGrounded())
        {
            isFalling = true;
        }

        #endregion

        #region Dashing Execution

        if(Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
        }

        #endregion

        Flip();
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
    }

    #region Dashing Function

    private IEnumerator Dash()
    {
        canDash = true;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    #endregion

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Flip()
    {
        if (isFacingRight && directionX < 0f || !isFacingRight && directionX > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}