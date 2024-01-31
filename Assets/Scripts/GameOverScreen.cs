using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text scoreText;
    public Text endText;
    public Text highScoreText;

    // The game over screen is setup with information
    // The reference of this script is stored as an object variable in the Inflate.cs script    
    public void Setup(string endText, int score, List<int> highScores) {
        gameObject.SetActive(true);
        this.scoreText.text = "SCORE: " + score.ToString();
        this.endText.text = endText;


        // Display high scores
        if (this.highScoreText != null)
        {
            this.highScoreText.text = "HIGH SCORES\n";

            // Display high scores as a comma-separated list
            this.highScoreText.text += string.Join(", ", highScores.Select((value, index) => $"{index + 1}: {value}"));
        }
        else
        {
            Debug.LogError("highScoreText == null");
        }

    }
}
