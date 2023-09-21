using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] SceneNames;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartScene( int index)
    {
        SceneManager.LoadScene(index);
    }
}
