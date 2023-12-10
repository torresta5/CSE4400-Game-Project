using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedHunterMovement : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] GameObject player;
    [SerializeField] GameObject hunterGun;
    [SerializeField] GameObject bullet;

    [SerializeField] Transform bulletSpawn;
    [SerializeField] float patrolRange = 10;
    
    [SerializeField] private bool flip;
    private Rigidbody2D rgbd;

    private Vector2 direction;

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
        
        patrolPoint = transform.position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(PlayerInAttackRange())
        {
            rgbd.velocity = Vector2.zero;
            FaceThePlayer();
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
        direction = transform.position - player.transform.position ;
        hunterGun.transform.right = direction;
        bulletSpawn.transform.right = direction;
        if(transform.localScale.x < 0)
        {
            bulletSpawn.right *= -1;
        }
    }

    private bool PlayerInAttackRange() 
    {
        return ( Vector2.Distance(transform.position, player.transform.position) < 20 ) ;
    }

    private void Attack()
    {
        if (!canShoot)
        {
            attackTime += Time.deltaTime;
            if(attackTime >= 3f)
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
}
