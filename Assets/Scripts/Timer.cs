using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    ///<summary>
    /// Reference to the text displaying the remaining time
    ///</summary>
    [SerializeField] private TextMeshProUGUI timerText;

    ///<summary>
    /// Remaining time
    ///</summary>
    [SerializeField] float remainingTime;

    ///<summary>
    /// Update the remaining time and load the lose scene if time runs out
    ///</summary>
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            SceneManager.LoadScene("Lose");
            Cursor.visible = true;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime - minutes * 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}