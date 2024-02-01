using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Charger la scène de jeu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReplayGame()
    {
        // Charger la scène de jeu
        SceneManager.LoadScene("General-scene");
    }

    public void MenuGame()
    {
        // Charger la scène de jeu
        SceneManager.LoadScene("Main scene");
    }

    public void QuitGame()
    {
        // Quitter l'application
        Application.Quit();
    }
}
