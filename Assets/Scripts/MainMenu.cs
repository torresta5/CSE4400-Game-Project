using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isJesus = false;

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
        Debug.Log("Jesus is here!");
    }

    public void JesusOffButton()
    {
        isJesus = false;
        Debug.Log("Farewell Jesus");
    }
}
