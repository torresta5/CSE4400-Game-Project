using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    public void nCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(30);
        }
    }

}
