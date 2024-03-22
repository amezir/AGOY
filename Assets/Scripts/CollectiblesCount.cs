using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectiblesCount : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text text;
    int collectiblesCount = 0;
    int totalCollectibles;

    void Start()
    {
        // Obtient le nombre total de collectibles dans la scène
        totalCollectibles = GameObject.FindGameObjectsWithTag("Collectible").Length;
    }

    public void IncreaseCollectiblesCount()
    {
        collectiblesCount++;
        text.text = collectiblesCount.ToString();

        // Vérifie si tous les collectibles ont été collectés
        if (collectiblesCount >= 6) // Vérifie si le nombre de collectibles collectés est égal à 6
        {
            // Charge la scène du menu
            SceneManager.LoadScene("Menu");
        }
    }
}
