using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isJesus = false;

    public string difficulty = "Normal";

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
        isJesus = true;
        //Debug.Log("Jesus is here!");
        StateNameController.isJesus = true;
        //Debug.Log(StateNameController.isJesus);
    }

    public void JesusOffButton()
    {
        isJesus = false;
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
}
