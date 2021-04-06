using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour
{
	// public variables
	public float secondsBetweenSpawning = 0.1f;
	public float xMinRange = -25.0f;
	public float xMaxRange = 25.0f;
	public float yMinRange = 8.0f;
	public float yMaxRange = 25.0f;
	public float zMinRange = -25.0f;
	public float zMaxRange = 25.0f;
	public GameObject[] spawnObjects; // what prefabs to spawn
	public GameObject[] powerupObjects; // Powerups to spawn
	public float powerupSpawnInterval = 5.0f; // seconds before the spawn of a powerup
	public Vector3 powerupSpawnpos; 

	private float nextSpawnTime;
	private float nextPowerupSpawnTime;


	// Use this for initialization
	void Start ()
	{
		// determine when to spawn the next object
		nextSpawnTime = Time.time+secondsBetweenSpawning;

		if(powerupObjects.Length != 0)
		{
			PowerupSpawner();
			nextPowerupSpawnTime = Time.time + powerupSpawnInterval;
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

		// if time to spawn a new game object
		if (Time.time  >= nextSpawnTime) {
			// Spawn the game object through function below
			MakeThingToSpawn ();

			// determine the next time to spawn the object
			nextSpawnTime = Time.time+secondsBetweenSpawning;
		}	

		if(Time.time >= nextPowerupSpawnTime)
		{
			PowerupSpawner();
			nextPowerupSpawnTime += powerupSpawnInterval;
		}
	}

	void MakeThingToSpawn ()
	{
		Vector3 spawnPosition; 

			// get a random position between the specified ranges
			spawnPosition.x = Random.Range(xMinRange, xMaxRange) + Random.Range(15, -15);
			spawnPosition.y = Random.Range(yMinRange, yMaxRange);
			spawnPosition.z = Random.Range(zMinRange, zMaxRange) + Random.Range(15, -15);
		
		// determine which object to spawn
		int objectToSpawn = Random.Range (0, spawnObjects.Length);

		// actually spawn the game object
		GameObject spawnedObject = Instantiate (spawnObjects [objectToSpawn], spawnPosition, transform.rotation) as GameObject;

		// make the parent the spawner so hierarchy doesn't get super messy
		spawnedObject.transform.parent = gameObject.transform;
	}
	void PowerupSpawner ()
	{
		if(GameObject.FindGameObjectWithTag("Powerup") == null && powerupObjects.Length != 0)
		{
			// Select a powerup randomly
				int selectPowerup = Random.Range(0, powerupObjects.Length);
				// Spawn powerup at start
				GameObject powerupObject = Instantiate(powerupObjects[selectPowerup], powerupSpawnpos, transform.rotation) as GameObject;
				powerupObject.transform.parent = gameObject.transform;
		}
		else
		{
			return;
		}
		
	}
}
