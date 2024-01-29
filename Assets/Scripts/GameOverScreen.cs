using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text scoreText;
    public Text endText;
    // Start is called before the first frame update
    
    public void Setup(string endText, int score) {
        gameObject.SetActive(true);
        this.scoreText.text = "SCORE: " + score.ToString();
        this.endText.text = endText;
    }
}
