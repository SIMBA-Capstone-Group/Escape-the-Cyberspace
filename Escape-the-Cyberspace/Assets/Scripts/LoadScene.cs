using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class LoadOpeningScene : MonoBehaviour
{
    public string sceneName;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
