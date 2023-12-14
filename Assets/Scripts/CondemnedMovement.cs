using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondemnedMovement : MonoBehaviour
{
    [SerializeField] private GameObject condemnedProjectile;
    [SerializeField] private Transform projectileSpawn; 
    private GameObject player;

    private float attackRange = 40;
    private float attackTime = 0;

    private bool canShoot = false;
    private bool flip = true;

    private AudioSource src;
    [SerializeReference] private AudioClip spit;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInAttackRange()) 
        {
            FaceThePlayer();
            Rotate();
            Attack();
        }
    }

    //returns true if the Player is in Attack Range
    private bool PlayerInAttackRange()
    {
        return (Vector2.Distance(transform.position, player.transform.position) <= attackRange);
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
        projectileSpawn.right = player.transform.position - projectileSpawn.position;
        if (!canShoot)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= 3f)
            {
                attackTime = 0;
                canShoot = true;
            }
        }
        else
        {
            src.clip = spit;
            src.Play();
            Instantiate(condemnedProjectile, projectileSpawn.position, projectileSpawn.rotation);
            canShoot = false;
        }
    }

    private void Rotate()
    {
        projectileSpawn.right = player.transform.position - transform.position;
        if (transform.localScale.x < 0)
        {
            projectileSpawn.transform.right *= -1;
        }
    }
}
