using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//Clock array
	public GameObject[] spawnableClocks;
	public static int gameHour = 12, gameMinute = 0;
	public int lives = 5;
	public int minClocks, maxClocks;
	public GameObject pauseMenu, gameOverMenu;

	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateUniversalClock", 1.0f, 5.0f);
		pauseMenu = GameObject.Find("PauseMenu");
		gameOverMenu = GameObject.Find("GameOverMenu");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			return;


	}

	void FixedUpdate () {
		//Running this depends on Timescale
		//Universal clock to sync w/
		Debug.Log(gameHour + ":" + gameMinute);
	}

	void UpdateUniversalClock () {
		gameMinute++;

		if (gameMinute >= 60)
		{
			gameMinute = 0;
			gameHour++;

			if (gameHour >= 13)
				gameHour = 1;
		}
	}

	void SpawnClock () {
		//Instantiate(spawnableClocks[Random.Range (0, spawnableClocks.Length)]);  position?
	}
	
	public void AddLife () {
		lives++;
	}

	public void LoseLife () {
		lives--;
		if (lives <= 0) {
			GameOver();
			//make button to try again or quit active; try again button reloads the scene.
		}
	}

	void GameOver () {
		Time.timeScale = 0;
		//gameOverMenu.SetActive(true);
	}
	public void Pause () {
		Time.timeScale = 0;
		//pauseMenu.SetActive(true);
	}

	public void Unpause () {
		//pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}
}
