using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManager gm;

	// public variables
	public int score=0;

	public Camera mainCamera;
	

	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public float startTime=5.0f;
	
	public Text mainScoreDisplay;
	public Text mainTimerDisplay;

	public GameObject gameOverScoreOutline;
	public GameObject gameOverScoreOutline2;

	public AudioSource musicAudioSource;

	public bool gameIsOver = false;

	public GameObject playAgainButtons;
	public string playAgainLevelToLoad;

	public GameObject nextLevelButtons;
	public string nextLevelToLoad;

	public GameObject restartLevelButtons;
	public string restartLevel;

	private float currentTime;

	// setup the game
	void Start () {

		// set the current time to the startTime specified
		currentTime = startTime;

		// get a reference to the GameManager component for use by other scripts
		if (gm == null) 
			gm = this.gameObject.GetComponent<GameManager>();

		// init scoreboard to 0
		mainScoreDisplay.text = "0";

		// inactivate the gameOverScoreOutline gameObject, if it is set
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (false);
		if (gameOverScoreOutline2)
			gameOverScoreOutline2.SetActive(false);

		// inactivate the playAgainButtons gameObject, if it is set
		if (playAgainButtons)
			playAgainButtons.SetActive (false);

		if (restartLevelButtons)
			restartLevelButtons.SetActive(false);

		// inactivate the nextLevelButtons gameObject, if it is set
		if (nextLevelButtons)
			nextLevelButtons.SetActive (false);

	}

	// this is the main game event loop
	void Update () {
		if (!gameIsOver) {
			if (canBeatLevel && (score >= beatLevelScore)) {  // check to see if beat game
				BeatLevel ();
			} else if (currentTime < 0) { // check to see if timer has run out
				EndGame ();
			} else { // game playing state, so update the timer
				currentTime -= Time.deltaTime;
				mainTimerDisplay.text = currentTime.ToString ("0.00");				
			}
		}
	}

	void EndGame() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "GAME OVER";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);
		if (gameOverScoreOutline2)
			gameOverScoreOutline2.SetActive(true);

		// activate the playAgainButtons gameObject, if it is set 
		if (playAgainButtons)
			playAgainButtons.SetActive (true);

		if (restartLevelButtons)
			restartLevelButtons.SetActive(true);

		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}
	
	void BeatLevel() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "LEVEL COMPLETE";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);
		if (gameOverScoreOutline2)
			gameOverScoreOutline2.SetActive(true);

		// activate the nextLevelButtons gameObject, if it is set 
		if (nextLevelButtons)
			nextLevelButtons.SetActive (true);

		if (playAgainButtons)
			playAgainButtons.SetActive(true);

		if (restartLevelButtons)
			restartLevelButtons.SetActive(true);

		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}

	// public function that can be called to update the score or time
	public void targetHit (int scoreAmount, float timeAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		score += scoreAmount;
		mainScoreDisplay.text = score.ToString ();
		
		// increase the time by the timeAmount
		currentTime += timeAmount;
		
		// don't let it go negative
		if (currentTime < 0)
			currentTime = 0.0f;

		// update the text UI
		mainTimerDisplay.text = currentTime.ToString ("0.00");
	}

	// Powerup pickup modification script
	public IEnumerator powerupEffect(float powerupDuration)
	{
		Debug.Log("Entered");
		Shooter powerupScript = mainCamera.GetComponent<Shooter>();
		powerupScript.strayFactor = 10;
		powerupScript.noOfProjectiles = 10;
		yield return new WaitForSeconds(powerupDuration);
		Debug.Log("Waiting done");
		powerupScript.noOfProjectiles = 1;
		powerupScript.strayFactor = 0;

	}

	// public function that can be called to restart the game
	public void RestartGame ()
	{
		// we are just loading a scene (or reloading this scene)
		// which is an easy way to restart the level
		SceneManager.LoadScene(playAgainLevelToLoad);
	}

	// public function that can be called to go to the next level of the game
	public void NextLevel ()
	{
		// we are just loading the specified next level (scene)
		SceneManager.LoadScene(nextLevelToLoad);
	}
	
	public void RestartLevel()
	{
		SceneManager.LoadScene(restartLevel);
	}

}
