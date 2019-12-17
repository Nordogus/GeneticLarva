using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reloadscene : MonoBehaviour
{
    private Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Reload()
    {
        currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(13);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
}
