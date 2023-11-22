using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            //Stop Moving/Translating
            rb.velocity = Vector3.zero;

            //Stop rotating
            rb.angularVelocity = Vector3.zero;
            playerHealth.takeDamage();
        }
    }
}
