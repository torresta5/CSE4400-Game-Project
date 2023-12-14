using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D rb;
    public float bulletSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, 2f);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            collision.gameObject.GetComponent<EnemyHealth>().playerController.AddMeter();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player") 
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            collision.gameObject.GetComponent<EnemyHealth>().playerController.AddMeter();
            Destroy(gameObject);
        }
    }
}
