using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour

{
    [SerializeField] AudioSource start;

    public void StartGame()
    {
        

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        start.Play();
    }
}
