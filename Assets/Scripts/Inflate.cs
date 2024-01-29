using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Inflate : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    // Variable to store the maximum amount of time the player can inflate the balloon
    private float maxTime = 3.25f;
    // A timer variable that will store the time the player has been inflating the balloon
    private float timer = 0.0f;
    // A boolean variable to store if the player has started inflating the balloon
    private bool started = false;
    // A boolean variable to store if the game is over
    private bool gameOver = false;
    // A reference to the balloon GameObject
    public GameObject balloon;
    // A reference to the tornBalloon GameObject
    public GameObject tornBalloon;
    // A reference to the AudioSource component, in our case the audio source is attached to the hot air balloon itself
    // What this means is that audio will play from that object. Since this is 2D, it doesn't really mean anything
    public AudioSource audioSource;
    // A reference to the inflateSound audio clip
    public AudioClip inflateSound;
    // A reference to the popSound audio clip
    public AudioClip popSound;
    private int score = 0;
    // A DateTime variable to store the time the player started inflating the balloon
    private DateTime _start;
    // Start is called before the first frame update
    void Start()
    {
        _start = DateTime.Now;
    }

    void Update() {
        // If the balloon GameObject exists and the player presses the left mouse button, 
        // the audioSource is not playing, and the game is not over, then the player has started inflating the balloon
        if(balloon && Input.GetKeyDown(KeyCode.Mouse0) && !audioSource.isPlaying && !gameOver) {
            _start = DateTime.Now;
            audioSource.PlayOneShot(inflateSound);
            started = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(balloon && Input.GetKey(KeyCode.Mouse0) && !gameOver) {
            // The difference in time stored in the _time variable
            // _start stores the time when the script starts execution, or the start of the game
            TimeSpan _time = DateTime.Now - _start;
            timer = _time.Seconds + _time.Milliseconds / 1000f;
            // This timer variable will check if the time since the player has pressed the mouse button
            // is within the maxTime that we defined (in this case 3.25 seconds).
            if(timer < maxTime) {
                // This line will increase the XYZ scale (size) of the balloon by the quantity presented on the right
                // It is only a small increment, but remember that this FixedUpdate() function is called every frame, so it stacks up
                balloon.transform.localScale += new Vector3(0.02f, 0.025f, 0f);
                // The reason the Y position of the balloon is increased, is because when the size of the balloon is increased,
                // it will grow bigger and cover up the platform since it grows from the "center" of the balloon.
                // Since we want the bottom of the balloon to be fixed in the same position, we increase the Y position
                // of the balloon by HALF of the amount we increase the Y size, so that we get an illusion that
                // the balloon is stuck to the bottom and growing from it.
                balloon.transform.position += new Vector3(0f, 0.0125f, 0f);
                // The score is set to be a ratio of the maximum size the balloon can get, converted between 0 and 1000
                score = (int)(timer / maxTime * 1000);
            } else {
                // If the player has been inflating the balloon for more than the maxTime, then the balloon will explode
                Destroy(balloon);
                Explode();
                started = false;
                gameOver = true;
                // The game over screen is displayed with the Game Over text if the balloon has exploded
                gameOverScreen.Setup("GAME OVER", 0);
                // audioSource.Stop();
            }
            // Debug.Log(balloon.transform.localScale);
        } else if(balloon && started) {
            // This is executed when the player has stopped inflating the balloon BEFORE it has exploded, which will give an actual score.
            gameOver = true;
            audioSource.Stop();
            gameOverScreen.Setup("NICE JOB!", score);
        }
    }

    void Explode() {
        // This code plays the pop sound effect, and created 10 torn balloons and throws them in random directions
        audioSource.PlayOneShot(popSound);
        CreateAndThrow(tornBalloon);
        CreateAndThrow(tornBalloon);
        CreateAndThrow(tornBalloon);
        CreateAndThrow(tornBalloon);
        CreateAndThrow(tornBalloon);
        CreateAndThrow(tornBalloon);
        CreateAndThrow(tornBalloon);
        CreateAndThrow(tornBalloon);
        CreateAndThrow(tornBalloon);
    }

    void CreateAndThrow(GameObject go) {
        // This will create a torn balloon at the exact same position the "balloon" was, which you can see in the arguments
        GameObject b = Instantiate(go, balloon.transform.position, balloon.transform.rotation);
        float randomX, randomY;
        randomX = UnityEngine.Random.Range(-5f, 5f);
        randomY = UnityEngine.Random.Range(4f, 6f);
        // For each torn balloon that's created, it's "rigidbody" component will be added with a force in a random direction
        b.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, randomY), ForceMode2D.Impulse);
    }

    public void RestartGame() {
        // This function is called EXTERNALLY by the Restart button in the GameOverScreen, not from within this code.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
