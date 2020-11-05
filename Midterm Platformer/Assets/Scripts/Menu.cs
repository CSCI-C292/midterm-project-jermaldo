using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Used this as reference https://www.youtube.com/watch?v=zc8ac_qUXQY
    public void PlayGame() 
    {
        SceneManager.LoadScene("Level1");
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
    public void HomeScreen()
    {
        SceneManager.LoadScene("Menu");
    }
}
