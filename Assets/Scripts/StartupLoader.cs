
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupLoader : MonoBehaviour
{
    
    void Start()
    {
        Invoke("LoadMainScene", 5);
    }

    void LoadMainScene()
    {
        SceneManager.LoadSceneAsync("Room");
    }

}
