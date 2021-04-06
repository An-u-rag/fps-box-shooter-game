using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{
    // target impact on game
    public bool isPowerup = false;
    public int scoreAmount = 0;
    public float timeAmount = 0.0f;
    public float powerupTime = 3.0f;

    Coroutine cor;


    // explosion when hit?
    public GameObject explosionPrefab;

    // when collided with another gameObject
    void OnCollisionEnter (Collision newCollision)
    {
        // exit if there is a game manager and the game is over
        if (GameManager.gm) {
            if (GameManager.gm.gameIsOver)
                return;
        }

        // only do stuff if hit by a projectile
        //if (newCollision.gameObject.tag == "Projectile") {
        if (newCollision.gameObject.CompareTag("Projectile") && !isPowerup) {
            if (explosionPrefab) {
                // Instantiate an explosion effect at the gameObjects position and rotation
                Instantiate (explosionPrefab, transform.position, transform.rotation);
            }

            // if game manager exists, make adjustments based on target properties
            if (GameManager.gm) {
                GameManager.gm.targetHit (scoreAmount, timeAmount);
            }
                
            // destroy the projectile
            Destroy (newCollision.gameObject);
                
            // destroy self
            Destroy (gameObject);
        }
        
            Debug.Log("Collision!");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isPowerup == true)
        {
            Debug.Log("Touched!");
            if (explosionPrefab)
            {
                // Instantiate an explosion effect at the gameObjects position and rotation
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            // if game manager exists, make adjustments based on target properties
            if (GameManager.gm)
            {
                Shooter scriptInstance = Camera.main.GetComponent<Shooter>();
                scriptInstance.StartCoroutine(GameManager.gm.powerupEffect(powerupTime));
            }

            // destroy self
            Destroy(gameObject);
        }
    }
}
