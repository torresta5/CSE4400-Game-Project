using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedHunterMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject hunterGun;
    [SerializeField] private GameObject bullet;

    [SerializeField] Transform bulletSpawn;
    [SerializeField] float patrolRange = 10f;
    [SerializeField] float attackRange = 20f;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private bool flip = true;
    private Rigidbody2D rgbd;

    private Vector2 direction;

    private float attackTime;
    
    private float patrolPoint;

    private bool canShoot = true;
    private bool patrol1 = true;
    private bool patrol2 = false;

    private AudioSource src;
    [SerializeReference] private AudioClip load, shoot;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        rgbd = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        patrolPoint = transform.position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(PlayerInAttackRange())
        {
            rgbd.velocity = new Vector2(0, rgbd.velocity.y);
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
        //direction = transform.position - player.transform.position;
        direction = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>().transform.position - bulletSpawn.transform.position;
        hunterGun.transform.right = direction;
        bulletSpawn.transform.right = direction;
        if(transform.localScale.x < 0)
        {
            hunterGun.transform.right *= -1;
            bulletSpawn.right *= -1;
        }
    }

    private bool PlayerInAttackRange() 
    {
        return (Vector2.Distance(transform.position, player.transform.position) < attackRange) ;
    }

    private void Attack()
    {
        if (!canShoot)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= 3f)
            {
                attackTime = 0;
                src.clip = load;
                src.Play();
                canShoot = true;
            }
        }
        else
        {
            src.clip = shoot;
            src.Play(0);
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            canShoot = false;
        }
    }

    private void Patrol()
    {
        if ((transform.localScale.x > 0 && patrol1) || (patrol2 && transform.localScale.x < 0))
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }

        if(patrol1)
        {
            rgbd.velocity = new Vector2(moveSpeed, rgbd.velocity.y);

            if(transform.position.x > patrolPoint + patrolRange)
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

        if (player.transform.position.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        } 
        else 
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }

        transform.localScale = scale;  
    }
}
