using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHunterMovement : MonoBehaviour
{ 
    [SerializeField] private float attackRange = 20f;
    [SerializeField] private float patrolRange = 10f;
    [SerializeField] private bool flip;
    private GameObject player;
    
    private Rigidbody2D rgbd;

    private float patrolPoint;
    private float moveSpeed = 7.5f;
    private float attackTime = 2f;

    private bool patrol1 = true;
    private bool patrol2 = false;
    private bool canAttack = false;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        patrolPoint = transform.position.x;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInAttackRange()) 
        {
            FaceThePlayer();
            Attack();        
        }
        else 
        {
            Patrol();
        }
    }
    
    //returns true if the Player is in Attack Range
    private bool PlayerInAttackRange()
    {
        return (Vector2.Distance(transform.position, player.transform.position) <= attackRange);
    }

    private void Patrol()
    {
        // makes sure the melee hunter is facing the correct direction
        if ((transform.localScale.x < 0 && patrol1) || (patrol2 && transform.localScale.x > 0))
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }

        if (patrol1)
        {
            rgbd.velocity = new Vector2(moveSpeed, rgbd.velocity.y);

            if (transform.position.x >= patrolPoint + patrolRange)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
                rgbd.velocity = Vector2.zero;
                patrol1 = !patrol1;
                patrol2 = !patrol2;
            }
        }
        else if (patrol2)
        {
            rgbd.velocity = new Vector2(-moveSpeed, rgbd.velocity.y);

            if (transform.position.x <= patrolPoint - patrolRange)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;

                rgbd.velocity = Vector2.zero;
                patrol1 = !patrol1;
                patrol2 = !patrol2;
            }
        }
    }

    // Function to make the Melee Hunter Face the Hunter as the name implies
    private void FaceThePlayer()
    {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 0);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }

        transform.localScale = scale;
    }

    private void Attack() 
    {
        // moves to the left if the Melee Hunter is to the right of the player and vice versa
        rgbd.velocity = new Vector2(moveSpeed * (transform.position.x >= player.transform.position.x ? -1: 1), rgbd.velocity.y);
        if (!canAttack)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= 2f)
            {
                attackTime = 0;
                canAttack = true;
            }
        }
        else
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x * 12.5f, rgbd.velocity.y);
            canAttack = false;
        }
    }
}
