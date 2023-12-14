using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        Destroy(gameObject, 0.05f);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(30);
        }
    }

}
