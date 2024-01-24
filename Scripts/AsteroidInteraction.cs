using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidInteraction : MonoBehaviour
{ 

    [SerializeField] AudioSource bump;
    [SerializeField] AudioSource gameOver;

   // private bool hasInteracted = false;
    private bool allowInteractions = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") && allowInteractions)
        {
            bump.Play();

        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destroy the current asteroid
        }

        
    }

}
