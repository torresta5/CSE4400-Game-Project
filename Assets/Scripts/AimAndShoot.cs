using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject[] weapons;
    
    [SerializeField] private float PISTOLCOOLDOWN = 1f;
    [SerializeField] private float LMGCOOLDOWN = 0.25f;
    [SerializeField] private float SHOTGUNCOOLDOWN = 2f;
    
    [SerializeField] private int numWeapons;
    [SerializeField] private int currentWeapon = 0;

    [SerializeField] private Transform[] bulletSpawn;

    private Vector2 mousePosition;
    private Vector2 direction;

    private bool canShoot = true;

    private float timeSinceShot = 0;

    private void Start()
    {
        numWeapons = weapons.Length;
        for (int i = 0; i < numWeapons; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[0].SetActive(true);

        gun = weapons[0];
    }

    private void Update()
    {
        SwitchWeapons();
        RotateToMouse();
        Shoot();
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
        timeSinceShot += Time.deltaTime;
        if (!canShoot)
        {
            switch (currentWeapon)
            {
                case 0:
                    if (timeSinceShot > PISTOLCOOLDOWN)
                    {
                        timeSinceShot = 0;
                        canShoot = true;
                    }
                    break;

                case 1:
                    if (timeSinceShot > LMGCOOLDOWN)
                    {
                        timeSinceShot = 0;
                        canShoot = true;
                    }
                    break;

                case 2:
                    if (timeSinceShot > SHOTGUNCOOLDOWN)
                    {
                        timeSinceShot = 0;
                        canShoot = true;
                    }
                    break;

                default:
                    break;
            }
        }
        if (Input.GetButton("Fire1") && canShoot)
        {
            if(currentWeapon ==2)
            {
                for(int i = currentWeapon; i < bulletSpawn.Length; i++)
                {
                    Instantiate(bullet, bulletSpawn[i].position, bulletSpawn[i].transform.rotation);
                }
            }
            else
            {
                Instantiate(bullet, bulletSpawn[currentWeapon].position, gun.transform.rotation);
            }
            canShoot = false;
        }
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

    private void SwitchWeapons() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon != 0)
        {
            weapons[0].SetActive(true);
            weapons[currentWeapon].SetActive(false);
            currentWeapon = 0;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapon != 1)
        {
            weapons[1].SetActive(true);
            weapons[currentWeapon].SetActive(false);
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && currentWeapon != 2)
        {
            weapons[2].SetActive(true);
            weapons[currentWeapon].SetActive(false);
            currentWeapon = 2;
        }
        gun = weapons[currentWeapon];
    }
}
