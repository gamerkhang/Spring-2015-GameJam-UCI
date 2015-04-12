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
	public static int lives = 5;
	public static int CurrentUniversalTime = 720;
	public static int currentAmountClocks, maxClocks = 1;
	public GameObject pauseMenu, gameOverMenu;
	public static float universalTickRate = 1f;
	public float clockSpawnRate = 30.0f;
	public float difficultyIncreaseRate = 60.0f;
	public int maxDifficulty = 8;
	
	// Use this for initialization
	void Start () {
		foreach (GameObject clock in spawnableClocks)
			clock.SetActive(false);
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
		livesText.text = lives.ToString();
		if (lives <= 0) {
			GameOver();
		}
		
		
		
	}
	
	void FixedUpdate () {
		//Running this depends on Timescale
	}
	
	void UpdateUniversalClock () {
		CurrentUniversalTime += 1;
		ParseTime (clockText, CurrentUniversalTime);
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
	
	void IncreaseMaxClocks() {
		if (maxClocks < maxDifficulty)
			maxClocks *= 2;
		else
			CancelInvoke("IncreaseMaxClocks");
	}
	
	public void AddLife () {
		lives++;
	}
	
	public static void LoseLife () {
		lives--;
		ScreenShake.shake = 1f;
	}
	
	public static int getUniversalTime (){
		return CurrentUniversalTime;
	}
	
	public void GameOver () {
		Time.timeScale = 0;
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
		Application.LoadLevel ("Gameplay");
	}
	
	public void Quit () {
		Application.Quit ();
	}
}