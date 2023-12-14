using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondemnedProjectile : MonoBehaviour
{

    private Animator anim;

    private Rigidbody2D rgbd;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            anim.SetBool("Explosion", true);
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
