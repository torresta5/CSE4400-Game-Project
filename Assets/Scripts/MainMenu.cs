using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isJesus = false;

    public string difficulty = "Normal";

    public GameObject extrasMenu;
    public GameObject levelSelectMenu;


    private void Awake()
    {
        if(!Cursor.visible)
        {
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if (extrasMenu.activeInHierarchy)
        {
            if (StateNameController.isComplete == false)
            {
                GameObject.Find("Secret").GetComponent<Button>().enabled = false;
            }
            else if (StateNameController.isComplete == true)
            {
                GameObject.Find("Secret").GetComponent<Button>().enabled = true;
            }
        }

        if (levelSelectMenu.activeInHierarchy)
        {
            if (StateNameController.level1Complete == false)
            {
                GameObject.Find("Level 1").GetComponent<Button>().enabled = false;
            }
            else if (StateNameController.level1Complete == true)
            {
                GameObject.Find("Level 1").GetComponent<Button>().enabled = true;
            }

            if (StateNameController.level2Complete == false)
            {
                GameObject.Find("Level 2").GetComponent<Button>().enabled = false;
            }
            else if (StateNameController.level2Complete == true)
            {
                GameObject.Find("Level 2").GetComponent<Button>().enabled = true;
            }

            if (StateNameController.level3Complete == false)
            {
                GameObject.Find("Level 3").GetComponent<Button>().enabled = false;
            }
            else if (StateNameController.level3Complete == true)
            {
                GameObject.Find("Level 3").GetComponent<Button>().enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            StateNameController.isComplete = !StateNameController.isComplete;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StateNameController.level1Complete = true;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            StateNameController.level2Complete = true;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            StateNameController.level3Complete = true;
        }
    }

    public void StartGame()
    {
       SceneManager.LoadSceneAsync(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void JesusOnButton()
    {
        StateNameController.isJesus = true;
    }

    public void JesusOffButton()
    {
        StateNameController.isJesus = false;
    }

    public void EasyButton()
    {
        difficulty = "Easy";
        StateNameController.difficulty = difficulty;
    }

    public void NormalButton()
    {
        difficulty = "Normal";
        StateNameController.difficulty = difficulty;
    }

    public void HardButton()
    {
        difficulty = "Hard";
        StateNameController.difficulty = difficulty;
    }

    public void LevelOneButton()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void LevelTwoButton()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void LevelThreeButton()
    {
        SceneManager.LoadSceneAsync(6);
    }


}
