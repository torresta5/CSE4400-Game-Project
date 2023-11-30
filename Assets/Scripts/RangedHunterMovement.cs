using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedHunterMovement : MonoBehaviour
{
    private GameObject player;

    [SerializeField] GameObject hunterGun;
    [SerializeField] GameObject bullet;

    [SerializeField] Transform bulletSpawn;

    private Rigidbody2D rgbd;

    private Vector2 direction;

    [SerializeField] float patrolRange = 10;
    private float attackTime;
    private float moveSpeed = 5;
    private float patrolPoint;

    private bool canShoot = true;
    private bool patrol1 = true;
    private bool patrol2 = false;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponentInParent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        patrolPoint = transform.position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        direction =  transform.position - player.transform.position;
        if(PlayerInAttackRange())
        {
            RotateWeapon();
            Attack();
        }
        else
        {
            Patrol();
        }
    }

    private void RotateWeapon()
    {
            hunterGun.transform.right = direction;
    }

    private bool PlayerInAttackRange() 
    {
        return (Vector2.Distance(transform.position, player.transform.position) > 10 && Vector2.Distance(transform.position, player.transform.position) < 20);
    }

    private void Attack()
    {
        if (!canShoot)
        {
            attackTime += Time.deltaTime;
            if(attackTime >= 10)
            {
                attackTime = 0;
                canShoot = true;
            }
        }
        else
        {
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            canShoot = false;
        }
    }

    private void Patrol()
    {
        if(patrol1)
        {
            rgbd.velocity = new Vector2(moveSpeed, rgbd.velocity.y);

            if(transform.position.x >= patrolPoint + patrolRange)
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
}
