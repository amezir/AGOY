using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Référence au texte affichant le temps restant
    [SerializeField] private TextMeshProUGUI timerText;
    // Temps restant
    [SerializeField] float remainingTime;

    void Update()
    {
        // Mettre à jour le temps restant
        if (remainingTime > 0)
        {
            // Décrémenter le temps restant
            remainingTime -= Time.deltaTime;
        }
        // Si le temps restant est inférieur à 0, le joueur a perdu
        else if (remainingTime < 0)
        {
            // Arrêter le temps restant
            remainingTime = 0;
            // Charger la scène de défaite
            SceneManager.LoadScene("Lose");
            // Afficher le curseur de la souris
            Cursor.visible = true;
        }
        // Mettre à jour le texte affichant le temps restant
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        // Récupérer les secondes restantes
        int seconds = Mathf.FloorToInt(remainingTime - minutes * 60);
        // Mettre à jour le texte affichant le temps restant
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
