using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{


    public GameObject asteroidPrefab;
    private int maxAsteroids = 10;
    private int asteroidsDestroyed = 0;
    private float maxSpeed = 1.5f;
    private float minSpeed = .8f;

    public static bool gameIsActive = true;

    private Animator anim;
    [SerializeField] AudioSource explosion;
    private AsteroidCounter asteroidCounter; // Reference to the AsteroidCounter script



        // Start is called before the first frame update
        void Start()
    {

        Screen.orientation = ScreenOrientation.Portrait; // Change this to your desired orientation

        InvokeRepeating("Spawner", 0f, 0.8f);

        asteroidCounter = FindObjectOfType<AsteroidCounter>(); // Finding and storing the reference to the AsteroidCounter script
        if (asteroidCounter == null)
        {
            Debug.LogError("AsteroidCounter script not found in the scene!");
        }
        anim = gameObject.GetComponent<Animator>();
    }


    void Spawner()
    {
        if (GameObject.FindGameObjectsWithTag("Asteroid").Length < maxAsteroids)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(6f, 7f), 0f);
            GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            // Set random rotation speed and movement speed for the newly created asteroid
            float randomRotationSpeed = Random.Range(50f, 150f);
            float randomMovementSpeed = Random.Range(minSpeed, maxSpeed);

            Rigidbody2D asteroidRB = newAsteroid.GetComponent<Rigidbody2D>();

            if (asteroidRB != null)
            {
                asteroidRB.angularVelocity = randomRotationSpeed; // Set the rotation speed
                asteroidRB.velocity = Vector2.down * randomMovementSpeed; // Set the movement speed downwards
            }


        }
        Debug.Log("Spawning");
    }





    // Update is called once per frame
    void Update()
    {
        {
            if (gameIsActive)
            {


                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0); // Get the first touch

                    if (touch.phase == TouchPhase.Began)
                    {
                        // Convert touch position from screen space to world space
                        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                        Vector2 touchPos2D = new Vector2(touchPos.x, touchPos.y);

                        // Use a raycast to detect collisions at the touch position
                        RaycastHit2D hit = Physics2D.Raycast(touchPos2D, Vector2.zero);

                        // Check if the raycast hits an object with the "Asteroid" tag
                        if (hit.collider != null && hit.collider.CompareTag("Asteroid"))
                        {

                            Animator asteroidAnimator = hit.collider.GetComponent<Animator>();
                            if (asteroidAnimator != null)
                            {
                                asteroidAnimator.SetTrigger("explosion");
                            }

                            // Destroy the asteroid that was touched
                            // Destroy(hit.collider.gameObject);
                            explosion.Play();

                            // Start the delay coroutine
                            StartCoroutine(DestroyAfterDelay(hit.collider.gameObject));
                        }
                    }
                }
            }
         }

        IEnumerator DestroyAfterDelay(GameObject asteroid)
        {
            // Wait 
            yield return new WaitForSeconds(.17f);

            // Destroy the asteroid after the delay
            Destroy(asteroid);
            Debug.Log("Destroyed");

            // Increment the count of destroyed asteroids using the AsteroidCounter script
            if (asteroidCounter != null)
            {
                asteroidCounter.IncrementAsteroidsDestroyed();

                // Check if the count of destroyed asteroids is a multiple of 10
                if (asteroidCounter.GetAsteroidsDestroyed() % 15 == 0)
                {
                    IncreaseMaxAsteroids(); // Call a method to increase maxAsteroids
                    maxSpeed += 0.2f;
                    minSpeed += 0.2f;
                }


            }
            else
            {
                Debug.LogWarning("AsteroidCounter reference is null!");
            }


        }


    }

    private void IncreaseMaxAsteroids()
    {
        maxAsteroids++; // Increment maxAsteroids by 1
        Debug.Log("Max Asteroids Increased: " + maxAsteroids);
    }

    public void ToggleGameActivity(bool isActive)
    {
        gameIsActive = isActive;
    }

}
