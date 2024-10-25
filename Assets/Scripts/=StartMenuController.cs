using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    // This method will be called when the Start Game button is pressed
    public void LoadMazeScene()
    {
        // Replace "MainScene" with the exact name of your scene
        SceneManager.LoadScene("MainScene");
    }
}