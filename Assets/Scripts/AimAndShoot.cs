using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    public GameObject weapon;
    public GameObject bullet;
    
    private Vector2 mousePosition;
    private Vector2 direction;
    
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
        direction = mousePosition - (Vector2)weapon.transform.position;
        weapon.transform.right = direction;
    }

    private void Shoot()
    {
            Instantiate(bullet, weapon.transform.position, weapon.transform.rotation);
    }

    private void FlipWeapon()
    { 
        if ((weapon.transform.position.x < 0) && (mousePosition.x < 0)) 
        {
            weapon.transform.up *= -1;
        }
        
        if(weapon.transform.position.x < 0  && mousePosition.x > 0) 
        {
            weapon.transform.up *= -1;
        }
    }
    
}
/*
    private bool canShoot = true;
    const float PISTOLSSHOTS = 1f;
    const float SMGSHOTS = 0.250f;
    const float SHOTGUNSHOTS = 2f;
    */