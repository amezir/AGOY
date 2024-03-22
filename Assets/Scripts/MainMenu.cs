using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Charger la scène de jeu
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("Jeu");
    }

    public void MenuGame()
    {
        SceneManager.LoadScene("Menu");
    }
    /// <summary>
    /// Quitter le jeu
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}