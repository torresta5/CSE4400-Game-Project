using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject nuke;

    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Image[] Loadout;
    [SerializeField] private Sprite[] inventory;
    
    [SerializeField] private float PISTOLCOOLDOWN = 1f;
    [SerializeField] private float LMGCOOLDOWN = 0.25f;
    [SerializeField] private float SHOTGUNCOOLDOWN = 2f;
    
    [SerializeField] private int numWeapons;
    [SerializeField] private int currentWeapon = 0;

    [SerializeField] private Transform[] bulletSpawn;

    [SerializeField] private AudioClip[] weaponShoot;
    private AudioSource audiosrc;
    private Vector2 mousePosition;
    private Vector2 direction;

    private bool canShoot = true;

    private float timeSinceShot = 0;

    public SpecialMeter specialMeter;

    [SerializeField] private GameObject crosshair;

    private void Awake()
    {
        Cursor.visible = false;
    }
    private void Start()
    {
        audiosrc = GetComponent<AudioSource>();
        
        numWeapons = weapons.Length;
        for (int i = 0; i < numWeapons; i++)
        {
            weapons[i].SetActive(false);
            Loadout[i].color = new (1f, 1f, 1f, 0.5f);
        }

        weapons[0].SetActive(true);
        Loadout[0].color = new(1f, 1f, 1f, 1f);
        gun = weapons[0];
    }

    private void Update()
    {
        SwitchWeapons();
        RotateToMouse();
        Shoot();
    }

    private void RotateToMouse()
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshair.transform.position = mousePosition; 
        direction = mousePosition - (Vector2)gun.transform.position;
        gun.transform.right = direction;

        for (int i = 0; i < bulletSpawn.Length; i++) 
        {
            
            bulletSpawn[i].right = direction;
        }

        if(transform.localScale.x < 0 ) 
        {
            gun.transform.right *= -1;
            for(int i = 0; i < bulletSpawn.Length; i++)
            {
                bulletSpawn[i].right *= -1;
            }
           
        }
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

                case 3:
                    if(specialMeter.slider.value == 35)
                    {
                        canShoot = true;
                    }
                    break;


                default:
                    break;
            }
        }
        // This is terrible, I know plz dont judge me
        if( (Input.GetButtonDown("Fire1") && canShoot && currentWeapon != 1)  || (Input.GetButton("Fire1") && canShoot && currentWeapon == 1))
        {
            canShoot = false;
            audiosrc.clip = weaponShoot[currentWeapon];
            // shotgun
            if (currentWeapon == 2)
            {
                audiosrc.Play();
                for(int i = currentWeapon; i < bulletSpawn.Length; i++)
                {
                    Instantiate(bullet, bulletSpawn[i].position, bulletSpawn[i].rotation);
                }
            }
            //holy cannon
            else if(currentWeapon == 3)
            {
                audiosrc.Play();
                
               
                Instantiate(nuke, bulletSpawn[currentWeapon].position, bulletSpawn[currentWeapon].rotation = new Quaternion(0, 0, 0, 0));

                weapons[0].SetActive(true);
                weapons[currentWeapon].SetActive(false);
                Loadout[currentWeapon].color = new(1f, 1f, 1f, 0.5f);
                currentWeapon = 0;
                specialMeter.slider.value = 0;

            }
            // pistol and LMG
            else
            {
                audiosrc.Play();
                Instantiate(bullet, bulletSpawn[currentWeapon].position, bulletSpawn[currentWeapon].rotation);
            }
        }
    }

    private void SwitchWeapons() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon != 0)
        {
            weapons[0].SetActive(true);
            weapons[currentWeapon].SetActive(false);
            Loadout[currentWeapon].color = new(1f, 1f, 1f, 0.5f);
            currentWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapon != 1 && StateNameController.level1Complete == true)
        {
            weapons[1].SetActive(true);
            weapons[currentWeapon].SetActive(false);
            Loadout[currentWeapon].color = new(1f, 1f, 1f, 0.5f);
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && currentWeapon != 2 && StateNameController.level2Complete == true)
        {
            weapons[2].SetActive(true);
            weapons[currentWeapon].SetActive(false);
            Loadout[currentWeapon].color = new(1f, 1f, 1f, 0.5f);
            currentWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && currentWeapon != 3 && specialMeter.slider.value == 35)
        {
            weapons[3].SetActive(true);
            weapons[currentWeapon].SetActive(false);
            Loadout[currentWeapon].color = new(1f, 1f, 1f, 0.5f);
            currentWeapon = 3;
        }
        gun = weapons[currentWeapon];
        Loadout[currentWeapon].color = new (1f, 1f, 1f, 1f);
    }
}
