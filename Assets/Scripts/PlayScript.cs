using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour
{
    public string playSceneName;
    public string howToPlaySceneName;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        //Debug.Log("Quitting game..."); 
        Application.Quit();
    }
}
