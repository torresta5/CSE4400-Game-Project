using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        
        if(tag == "Boss" && currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadSceneAsync(9);
            StateNameController.isComplete = true;
        }

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
