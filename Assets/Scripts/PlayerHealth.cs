using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    
    public int health;

    
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        health = numOfHearts;
    }

    private void Update()
    {
        // If statement so your health cannot exceed the number of Hearts
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        
        for (int i = 0; i < hearts.Length; i++)
        {
            // changes the hearts to Empty or Full
            if (i < health)
            {
                hearts[i].sprite = fullHeart; 
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            // Changes the number of hearts displayed
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else 
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void takeDamage()
    {
        health -= 1;
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1.0f;
        }
    }
}
