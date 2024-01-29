using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inflate : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    private float maxTime = 3.25f;
    private float timer = 0.0f;
    private bool started = false;
    private bool gameOver = false;
    public GameObject balloon;
    public GameObject tornBalloon;
    public AudioSource audioSource;
    public AudioClip inflateSound;
    public AudioClip popSound;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(balloon && Input.GetKeyDown(KeyCode.Mouse0) && !audioSource.isPlaying && !gameOver) {
            audioSource.PlayOneShot(inflateSound);
            started = true;
        }
        if(balloon && Input.GetKey(KeyCode.Mouse0) && !gameOver) {
            timer += Time.deltaTime;
            if(timer < maxTime) {
                balloon.transform.localScale += new Vector3(0.0004f, 0.0006f, 0f);
                balloon.transform.position += new Vector3(0f, 0.0003f, 0f);
                score = (int)(timer / maxTime * 1000);
            } else {
                Destroy(balloon);
                Explode();
                started = false;
                gameOver = true;
                gameOverScreen.Setup("GAME OVER", 0);
                // audioSource.Stop();
            }
            // Debug.Log(balloon.transform.localScale);
        } else if(balloon && started) {
            gameOver = true;
            audioSource.Stop();
            gameOverScreen.Setup("NICE JOB!", score);
        }
    }

    void Explode() {
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
        GameObject b = Instantiate(go, balloon.transform.position, balloon.transform.rotation);
        float randomX, randomY;
        randomX = Random.Range(-5f, 5f);
        randomY = Random.Range(4f, 6f);
        b.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, randomY), ForceMode2D.Impulse);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
