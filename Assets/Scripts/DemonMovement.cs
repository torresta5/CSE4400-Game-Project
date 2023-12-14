using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.EventSystems;
using UnityEngine;

public class DemonMovement : MonoBehaviour
{
    private PlayerController playerController;
    private Rigidbody2D rb;

    public float moveSpeed;
    public float playerRange;
    public LayerMask playerLayer;
    public bool playerInRange;
    public Transform startingPoint;

    public void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);
        if (playerInRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerController.transform.position, moveSpeed * Time.deltaTime);
        }
        else if(!playerInRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPoint.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
    }
}
