using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private Camera mainCamera;

    [HideInInspector] public bool isFacingLeft;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private float directionX;

    private bool isJumping;
    private bool isFalling;
    private float jumpTimeCounter;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 3f;
    private bool isFacingRight = true;
    private Animator anim;
    private Vector3 mouseWorldPosition;

    public int jumpSpeed;
    public float runSpeed = 15f;
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool knockFromRight;
    public SpecialMeter specialMeter;
    public int currentMeter;
    public Bullet bullet;

    public GameObject beard;
    public GameObject robe;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        Debug.Log(StateNameController.isJesus);

        if(StateNameController.isJesus == false)
        {
            beard.SetActive(false);
            robe.SetActive(false);
            Debug.Log(StateNameController.isJesus);
        }

        currentMeter = 0;
        specialMeter.SetMinMeter();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        //Debug.Log(mouseWorldPosition);

        directionX = Input.GetAxisRaw("Horizontal");

        if(KBCounter <= 0)
        {
            rb.velocity = new Vector2(directionX * runSpeed, rb.velocity.y);
        }
        else
        {
            if(knockFromRight == true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if(knockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
        }

        #region Jumping

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.gravityScale = 5;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            //anim.SetBool("jumping", true);

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
            anim.SetBool("jumping", true);
            if (rb.gravityScale < 6)
            {
                rb.gravityScale += Time.deltaTime;
                if (IsGrounded())
                {
                    anim.SetBool("falling", false);
                    anim.SetBool("jumping", false);
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

        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
        }

        #endregion

        #region Animations

        if (directionX > 0)
        {
            anim.SetBool("running", true);
        }
        else if (directionX < 0)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }

        #endregion

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
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
        if (isFacingRight && mouseWorldPosition.x < transform.position.x || !isFacingRight && mouseWorldPosition.x > transform.position.x)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        //if (isFacingRight && directionX < 0f || !isFacingRight && directionX > 0f)
        //{
        //    Vector3 localScale = transform.localScale;
        //    isFacingRight = !isFacingRight;
        //    localScale.x *= -1f;
        //    transform.localScale = localScale;
        //}
    }

    public void AddMeter()
    {
        specialMeter.SetMeter(1);
    }
}