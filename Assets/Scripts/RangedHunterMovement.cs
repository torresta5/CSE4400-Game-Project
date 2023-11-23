using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedHunterMovement : MonoBehaviour
{
    private GameObject player;

    [SerializeField] GameObject hunterGun;

    private Rigidbody2D rgbd;

    private Vector2 direction;

    private float moveSpeed = 5;
    private float patrolPoint1;
    private float patrolPoint2;

    private bool patrol1 = true;
    private bool patrol2 = false;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponentInParent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        patrolPoint1 = transform.position.x + 10;
        patrolPoint2 = transform.position.x - 10;
    }

    // Update is called once per frame
    void Update()
    {
        
        Patrol();
        RotateWeapon();
    }

    private void RotateWeapon()
    {
        direction =  player.transform.position - hunterGun.transform.position;

        if(Vector2.Distance(hunterGun.transform.position, player.transform.position) < 10)
        {
            hunterGun.transform.right = direction;
        }
    }

    private void Patrol()
    {
        
        if(patrol1)
        {
            rgbd.velocity = new Vector2(moveSpeed, rgbd.velocity.y);

            if(transform.position.x >= patrolPoint1)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;

                rgbd.velocity = Vector2.zero;
                patrol1 = !patrol1;
                patrol2 = !patrol2;
            }
        }
        else if(patrol2)
        {
            rgbd.velocity = new Vector2(-moveSpeed, rgbd.velocity.y);

            if (transform.position.x <= patrolPoint2)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;

                patrol1 = !patrol1;
                patrol2 = !patrol2;
            }
        }
    }
}
