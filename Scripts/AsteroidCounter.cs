using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidCounter : MonoBehaviour
{
    private static int asteroidsDestroyed = 0;
    [SerializeField] private Text asteroidText;
    [SerializeField] private Text finalScore;

    // Method to increment the count of destroyed asteroids
    public void IncrementAsteroidsDestroyed()
    {
        asteroidsDestroyed++;
        // Update the UI or perform any action to display the count
        Debug.Log("Asteroids Destroyed: " + asteroidsDestroyed);
        asteroidText.text = "Asteroids:" + asteroidsDestroyed;


    }



    // Method to retrieve the count of destroyed asteroids
    public int GetAsteroidsDestroyed()
    {
        return asteroidsDestroyed;
    }

    // Method to reset the count of destroyed asteroids to zero
    public void ResetAsteroidsDestroyed()
    {
        asteroidsDestroyed = 0;
        //asteroidText.text = "Asteroids: 0"; // Reset the UI text to display zero
    }

}
