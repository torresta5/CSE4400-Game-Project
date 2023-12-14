using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatMovement : MonoBehaviour
{
    private PlayerController playerController;
    private Rigidbody2D rb;

    public float moveSpeed;
    public float playerRange;
    public LayerMask playerLayer;
    public bool playerInRange;
    public Transform startingPoint;
    

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawn;
    public float attackTime = 0;
    private bool canShoot = false;
    private bool flip = true;
    private GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FaceThePlayer();
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);
        if (playerInRange)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, playerController.transform.position, moveSpeed * Time.deltaTime);
        }
        else if (!playerInRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPoint.position, moveSpeed * Time.deltaTime);
        }
        
        projectileSpawn.right = player.GetComponent<Collider2D>().transform.position - projectileSpawn.position;
        if (!canShoot)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= 1f)
            {
                attackTime = 0;
                canShoot = true;
            }
        }
        else
        {
            Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
            canShoot = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
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
