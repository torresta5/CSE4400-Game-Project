using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] float patrolRange = 10;

    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    private bool patrol1 = true;
    private bool patrol2 = false;
    private float patrolPoint;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        patrolPoint = transform.position.x;
    }

    void Update()
    {
        //if (patrolDestination == 0)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
        //    if (Vector2.Distance(transform.position, patrolPoints[0].position) < .5f)
        //    {
        //        patrolDestination = 1;
        //    }
        //}

        //if (patrolDestination == 1)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
        //    if (Vector2.Distance(transform.position, patrolPoints[0].position) < .5f)
        //    {
        //        patrolDestination = 0;
        //    }
        //}

        Patrol();

    }

    private void Patrol()
    {
        if (patrol1)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            if (transform.position.x >= patrolPoint + patrolRange)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;

                rb.velocity = Vector2.zero;
                patrol1 = !patrol1;
                patrol2 = !patrol2;
            }
        }
        else if (patrol2)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            if (transform.position.x <= patrolPoint - patrolRange)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;

                rb.velocity = Vector2.zero;
                patrol1 = !patrol1;
                patrol2 = !patrol2;
            }
        }
    }
}
