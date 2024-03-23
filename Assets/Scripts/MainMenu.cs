using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Load the game scene
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /// <summary>
    /// Replay the game from the beginning
    /// </summary>
    public void ReplayGame()
    {
        SceneManager.LoadScene("Jeu");
    }
    /// <summary>
    /// Return to the main menu
    /// </summary>
    public void MenuGame()
    {
        SceneManager.LoadScene("Menu");
    }
    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
