using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Ground : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text scoreText;
    private AsteroidCounter asteroidCounter;
    [SerializeField] AudioSource gameOver;
    GameManagement gameManger;
    private bool gameOverSoundPlayed = false;
    

    private void Start()
    {
        asteroidCounter = FindObjectOfType<AsteroidCounter>();
        if (asteroidCounter == null)
        {
            Debug.LogError("AsteroidCounter not found!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject); // Destroy the current GameObject (the asteroid)
            Debug.Log("Asteroid has hit");

            // Display Game Over UI
            gameOverPanel.SetActive(true);
            

            //gameOver.Play();


            if (!gameOverSoundPlayed)
            {
                gameOver.Play();
                gameOverSoundPlayed = true; // Update the flag to indicate the sound has been played
            }
            else
            {
                gameOver.mute = true; // Mute the game over sound if it has already been played
            }



            // Get the score from AsteroidCounter
            if (asteroidCounter != null)
            {
                int currentScore = asteroidCounter.GetAsteroidsDestroyed();
                scoreText.text = "Score: " + currentScore;
            }

            GameManagement gameManager = FindObjectOfType<GameManagement>();
            if (gameManager != null)
            {
                gameManager.ToggleGameActivity(false);
            }
        }
    }


 

    // Update is called once per frame
    void Update()
    {
        
    }
}
