using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;
    public static SceneLoader Instance { get { return _instance; } }

    int CurrentScene = 0;

    private void Awake()
    {
        //create this object as a singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            CurrentScene = SceneManager.GetActiveScene().buildIndex;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void LoadScene(int i)
    {
        CurrentScene = i;
        SceneManager.LoadScene(i);
    }

    public void LoadNextScene()
    {
        CurrentScene++;
        if(CurrentScene >= SceneManager.sceneCountInBuildSettings)
        {
            //Goto Main Menu
            CurrentScene = 0;
        }
        SceneManager.LoadScene(CurrentScene);
    }
}
