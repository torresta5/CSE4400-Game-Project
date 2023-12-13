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

    public void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.V))
        {
            StateNameController.isComplete = !StateNameController.isComplete;
        }
    }

    public void StartGame()
    {
       SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void JesusOnButton()
    {
        //isJesus = true;
        //Debug.Log("Jesus is here!");
        StateNameController.isJesus = true;
        //Debug.Log(StateNameController.isJesus);
    }

    public void JesusOffButton()
    {
        //isJesus = false;
        //Debug.Log("Farewell Jesus");
        StateNameController.isJesus = false;
        //Debug.Log(StateNameController.isJesus);
    }

    public void EasyButton()
    {
        difficulty = "Easy";
        //Debug.Log(difficulty);
        StateNameController.difficulty = difficulty;
    }

    public void NormalButton()
    {
        difficulty = "Normal";
        //Debug.Log(difficulty);
        StateNameController.difficulty = difficulty;
    }

    public void HardButton()
    {
        difficulty = "Hard";
        //Debug.Log(difficulty);
        StateNameController.difficulty = difficulty;
    }

    public void LevelOneButton()
    {
        SceneManager.LoadSceneAsync(1);
    }


}
