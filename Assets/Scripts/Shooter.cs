using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	// Reference to projectile prefab to shoot
	public GameObject projectile;
	public float power = 10.0f;
	public int noOfProjectiles = 3;
	public float strayFactor = 0.4f;

	// Reference to AudioClip to play
	public AudioClip shootSFX;
	

	// Update is called once per frame
	void Update () {
		// Detect if fire button is pressed
		if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
		{	
			// if projectile is specified
			if (projectile)
			{
				SpawnProjectiles(noOfProjectiles);
				/*
				// Instantiante projectile at the camera + 1 meter forward with camera rotation
				GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward, transform.rotation) as GameObject;

				// if the projectile does not have a rigidbody component, add one
				if (!newProjectile.GetComponent<Rigidbody>()) 
				{
					newProjectile.AddComponent<Rigidbody>();
				}
				// Apply force to the newProjectile's Rigidbody component if it has one
				newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange);
				
				// play sound effect if set
				if (shootSFX)
				{
					if (newProjectile.GetComponent<AudioSource> ()) { // the projectile has an AudioSource component
						// play the sound clip through the AudioSource component on the gameobject.
						// note: The audio will travel with the gameobject.
						newProjectile.GetComponent<AudioSource> ().PlayOneShot (shootSFX);
					} else {
						// dynamically create a new gameObject with an AudioSource
						// this automatically destroys itself once the audio is done
						AudioSource.PlayClipAtPoint (shootSFX, newProjectile.transform.position);
					}
				}*/
			}
		}
	}

	void SpawnProjectiles(int numberOfProjectiles)
	{
		GameObject newProjectile = Instantiate(projectile, transform.position +transform.forward, transform.rotation) as GameObject;
		newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * power, ForceMode.VelocityChange);
		for (int i = 0; i <= noOfProjectiles-2; i++)
		{

			var randomNumberX = Random.Range(-strayFactor, strayFactor);
			var randomNumberY = Random.Range(-strayFactor, strayFactor);
			var randomNumberZ = Random.Range(-strayFactor, strayFactor);
			

			// Instantiante projectile at the camera + 1 meter forward with camera rotation
			newProjectile = Instantiate(projectile, transform.position + transform.forward, transform.rotation) as GameObject;

			newProjectile.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);

			if (!newProjectile.GetComponent<Rigidbody>())
			{
				newProjectile.AddComponent<Rigidbody>();
			}

			// Apply force to the newProjectile's Rigidbody component if it has one
			newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * power, ForceMode.VelocityChange);


		}


		if (shootSFX)
		{
			if (newProjectile.GetComponent<AudioSource>())
			{ // the projectile has an AudioSource component
			  // play the sound clip through the AudioSource component on the gameobject.
			  // note: The audio will travel with the gameobject.
				newProjectile.GetComponent<AudioSource>().PlayOneShot(shootSFX);
			}
			else
			{
				// dynamically create a new gameObject with an AudioSource
				// this automatically destroys itself once the audio is done
				AudioSource.PlayClipAtPoint(shootSFX, newProjectile.transform.position);
			}
		}
	}
}
