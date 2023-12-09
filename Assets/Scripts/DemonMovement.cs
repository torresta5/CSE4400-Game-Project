using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMovement : MonoBehaviour
{

    public float speed;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        Chase();
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
