using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondemnedProjectile : MonoBehaviour
{
    private Rigidbody2D rgbd;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rgbd.velocity = transform.right * 25;
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if collide with Player or Ground layer
        if (collision.gameObject.CompareTag("Player")) 
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 4f);
            if (collider.gameObject.CompareTag("Player")) 
            {
                collider.gameObject.GetComponent<PlayerHealth>().takeDamage();
            }
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage();
            Destroy(gameObject);    
        }
        else if (collision.gameObject.layer == 6) 
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 4f);
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<PlayerHealth>().takeDamage();
            }
            Destroy(gameObject);
        }
    }
}
