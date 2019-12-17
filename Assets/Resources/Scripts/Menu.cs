using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
   
    public void StartGame(int levelid)
    {
        SceneManager.LoadScene(levelid);
    }
     public void QuitGame()
    {
        Application.Quit();
    }
    
}

