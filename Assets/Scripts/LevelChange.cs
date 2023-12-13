using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public int sceneBuildIndex;

    public int level;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
            if(level == 1)
            {
                StateNameController.level1Complete = true;
            }
            else if (level == 2)
            {
                StateNameController.level2Complete = true;
            }
            else if (level == 3)
            {
                StateNameController.level3Complete = true;
            }


        }


    }

}
