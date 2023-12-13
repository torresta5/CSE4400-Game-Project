using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneChanger : MonoBehaviour
{

    public float changeTime;
    public string sceneName;

    // Update is called once per frame
    private void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <= 0)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        if(Input.GetKeyDown(KeyCode.Return)) 
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
