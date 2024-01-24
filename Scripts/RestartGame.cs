using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] AudioSource reload;
    [SerializeField] private AsteroidCounter asteroidCounter; // Reference to the AsteroidCounter script

    public void Restart()
    {
        // Reset the asteroids destroyed count
        if (asteroidCounter != null)
        {
            asteroidCounter.ResetAsteroidsDestroyed();
        }
        GameManagement.gameIsActive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);


        reload.Play();
    }
}
