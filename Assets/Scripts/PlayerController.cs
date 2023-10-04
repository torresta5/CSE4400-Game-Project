using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float jumpTime = 0.5f;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private bool isJumping;
    private bool isFalling;
    private float jumpTimeCounter;

    public int jumpSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(directionX * 7f, rb.velocity.y);

        #region Jumping

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
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

        #endregion
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
