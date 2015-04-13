using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	//Clock array
	public GameObject[] spawnableClocks;
	public Text clockText;
	public Text livesText;
	int gameHour = 0, gameMinute = 0;
	public int lives = 5;
	public int OriginalUniversalTime = 720;
	public int CurrentUniversalTime = 720;
	public int VictoryTime = 1440;
	public int timeSurvived;
	public int currentAmountClocks, minClocks = 1, maxClocks = 1;
	public GameObject pauseMenu, gameOverMenu;
	public static float universalTickRate = 1f;
	public float clockSpawnRate = 30.0f;
	public float difficultyIncreaseRate = 60.0f;
	public int maxLives = 3;
	public int maxDifficulty = 8;
	public int streakAmount;
	public int highestStreak;
	public GameObject streakWindow;
	public Text streakText;
	public Text gameOverText;
	
	// Use this for initialization
	void Start () {
		CurrentUniversalTime = 720;
		maxClocks = 1;
		lives = 5;
		foreach (GameObject clock in spawnableClocks) {
			if (clock.name == "ship")
			{
				clock.GetComponent<Clock>().StartClock();
				currentAmountClocks++;
			}
			else
				clock.SetActive (false);
		}
		InvokeRepeating("UpdateUniversalClock", 0f, universalTickRate);
		InvokeRepeating("AddClock", 0f, clockSpawnRate);
		InvokeRepeating("IncreaseMaxClocks", 0f, difficultyIncreaseRate);
		pauseMenu.SetActive(false);
		gameOverMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause"))
		{
			if (Time.timeScale == 0)
				Unpause();
			else
				Pause ();
		}
		
		if (Time.timeScale == 0)
			return;
		livesText.text = "x " + lives.ToString();
		if (lives <= 0) {
			Invoke("GameOver", 1f);
		}
		
		
		
	}
	
	void FixedUpdate () {
		//Running this depends on Timescale
	}
	
	void UpdateUniversalClock () {
		CurrentUniversalTime += 1;
		if (CurrentUniversalTime == VictoryTime)
			Victory();
		ParseTime (clockText, CurrentUniversalTime);
	}

	void Victory() {

	}
	
	void ParseTime(Text t, int minutes){
		gameHour = minutes / 60;
		if (gameHour > 12) {
			gameHour -=(12*(gameHour/12));
		}
		gameMinute = minutes % 60;
		
		t.text = string.Format ("{0:D2}:{1:D2}", gameHour, gameMinute);
	}
	
	
	void AddClock () {
		if (currentAmountClocks < maxClocks)
		{
			List<GameObject> inactives = new List<GameObject>();
			foreach (GameObject clock in spawnableClocks)
			{
				if (!clock.activeInHierarchy)
					inactives.Add(clock);
			}
			if (inactives.Count > 0)
			{
				int clockIndex = Random.Range (0, inactives.Count);
				Debug.Log (clockIndex);
				inactives[clockIndex].SetActive(true);	
				inactives[clockIndex].GetComponent<Clock>().StartClock();
				currentAmountClocks++;
			}
		}
		//Instantiate(spawnableClocks[Random.Range (0, spawnableClocks.Length)]);  position?
		//set the chosen one active
	}

	public void RemoveClock() {
		currentAmountClocks--;
		if (currentAmountClocks < minClocks)
			AddClock();
	}
	
	void IncreaseMaxClocks() {
		if (maxClocks < maxDifficulty)
		{
			if (!(minClocks == 1 && maxClocks == 1))
				minClocks *= 2;
			maxClocks *= 2;
		}
		//if (lives < maxLives)
		//	lives++;
	}

	public void AddtoStreak() {
		streakAmount++;
		if (streakAmount > highestStreak)
			highestStreak = streakAmount;
		if (streakAmount > 1)
		{
			streakText.text = streakAmount + " Streak!";
			streakWindow.SetActive(true);
			Invoke ("HideStreak", 2.0f);
		}
	}

	public void HideStreak(){
		streakWindow.SetActive(false);
	}

	public void AddLife () {
		lives++;
	}
	
	public void LoseLife () {
		lives--;
		ScreenShake.shake = 1f;
	}
	
	public void GameOver () {
		int survived = CurrentUniversalTime - OriginalUniversalTime;
		gameOverText.text = string.Format ("You survived for \n{0:D2} minutes and\n {1:D2} seconds...\nHighest streak: {2}",
		                                   survived / 60, survived % 60, highestStreak);

		minClocks = 1;
		maxClocks = 1;
		CancelInvoke("UpdateUniversalClock");
		CancelInvoke("AddClock");
		CancelInvoke("IncreaseMaxClocks");
		Time.timeScale = 0;
		ScreenShake.shake = 0f;
		gameOverMenu.SetActive(true);
	}

	public void Pause () {
		Time.timeScale = 0;
		pauseMenu.SetActive(true);
	}
	
	public void Unpause () {
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}
	
	public void TryAgain () {
		Time.timeScale = 1;
		Application.LoadLevel (Application.loadedLevelName);
	}
	
	public void Quit () {
		Application.LoadLevel ("MainMenu");
	}
}