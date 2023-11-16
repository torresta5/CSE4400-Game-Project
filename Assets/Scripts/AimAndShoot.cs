using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject[] weapons;

    [SerializeField] private int numWeapons;
    [SerializeField] private int currentWeapon = 0;

    [SerializeField] private Transform[] bulletSpawn;

    private Vector2 mousePosition;
    private Vector2 direction;

    private bool canShoot = true;

    [SerializeField] private float PISTOLCOOLDOWN = 1f;
    [SerializeField] private float LMGCOOLDOWN = 0.25f;
    [SerializeField] private float SHOTGUNCOOLDOWN = 2f;
    private float timeSinceShot = 0;

    private void Update()
    {
        RotateToMouse();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        FlipWeapon();
    }

    private void RotateToMouse()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - (Vector2)gun.transform.position;
        gun.transform.right = direction;
    }

    private void Shoot()
    {
            Instantiate(bullet, gun.transform.position, gun.transform.rotation);
    }

    private void FlipWeapon()
    { 
        if ((gun.transform.position.x < 0) && (mousePosition.x < 0)) 
        {
            gun.transform.up *= -1;
        }
        
        if(gun.transform.position.x < 0  && mousePosition.x > 0) 
        {
            gun.transform.up *= -1;
        }
    }
    
}
/*
    private bool canShoot = true;
    const float PISTOLSSHOTS = 1f;
    const float SMGSHOTS = 0.250f;
    const float SHOTGUNSHOTS = 2f;
    */