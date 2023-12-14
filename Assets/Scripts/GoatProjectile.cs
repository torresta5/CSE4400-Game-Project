using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * 125;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if collide with Player or Ground layer
        if (collision.gameObject.CompareTag("Player"))
        {
            //anim.SetBool("Explosion", true);
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 4f);
            /*
            if (collider.gameObject.CompareTag("Player")) 
            {
                collider.gameObject.GetComponent<PlayerHealth>().takeDamage();
            }
            */
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage();
            Destroy(gameObject);
            //anim.SetBool("Explosion", false);
        }
        else if (collision.gameObject.layer == 6)
        {
            //anim.SetBool("Explosion", true);
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 4f);
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<PlayerHealth>().takeDamage();
            }
            Destroy(gameObject);
            //anim.SetBool("Explosion", false);
        }
    }
}
