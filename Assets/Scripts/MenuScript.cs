using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Load main scene
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // Exit the application
    public void ExitGame()
    {
        Application.Quit();
    }
}
