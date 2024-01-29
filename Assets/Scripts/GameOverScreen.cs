using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text scoreText;
    public Text endText;

    // The game over screen is setup with information
    // The reference of this script is stored as an object variable in the Inflate.cs script    
    public void Setup(string endText, int score) {
        gameObject.SetActive(true);
        this.scoreText.text = "SCORE: " + score.ToString();
        this.endText.text = endText;
    }
}
