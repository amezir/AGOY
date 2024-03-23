using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectiblesCount : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text text;
    int collectiblesCount = 0;
    int totalCollectibles;

    ///<summary>
    /// Retrieve the total number of collectibles in the scene
    ///</summary>
    void Start()
    {
        totalCollectibles = GameObject.FindGameObjectsWithTag("Collectible").Length;
    }

    ///<summary>
    /// Increment the count of collected collectibles and update the text displaying the number of collected collectibles
    ///</summary>
    public void IncreaseCollectiblesCount()
    {
        collectiblesCount++;
        text.text = collectiblesCount.ToString();

        if (collectiblesCount >= totalCollectibles)
        {
            SceneManager.LoadScene("Menu");
            Cursor.visible = true;
        }
    }
}
