using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	//Clock array
	public GameObject[] spawnableClocks;
	public Text clockText;
	public Text livesText;
	int gameHour = 0, gameMinute = 0;
	public static int lives = 5;
	public static int CurrentUniversalTime = 720;
	public int minClocks, maxClocks;
	public GameObject pauseMenu, gameOverMenu;
	public static float universalTickRate;

	// Use this for initialization
	void Start () {
		universalTickRate = 0.2f;
//		clockText = GameObject.Find ("Clock").GetComponent<Text>();
//		livesText = GameObject.Find ("Lives").GetComponent<Text>();
		InvokeRepeating("UpdateUniversalClock", 0f, universalTickRate);
		pauseMenu = GameObject.Find("PauseMenu");
		gameOverMenu = GameObject.Find("GameOverMenu");
		gameOverMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			return;

		//ShowClock();
		livesText.text = lives.ToString();
		
		if (lives <= 0) {
			GameOver();
			//make button to try again or quit active; try again button reloads the scene.
		}

	}

	void FixedUpdate () {
		//Running this depends on Timescale
		//Universal clock to sync w/
		//Debug.Log(gameHour + ":" + gameMinute);
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
		string minute="";
		if(gameMinute<10)
			minute = "0";
		t.text = string.Format ("{0:D2}:{1:D2}", gameHour, gameMinute);
	}

//	void ShowClock () {
//		clockText.text = 
//	}

	void SpawnClock () {
		//Instantiate(spawnableClocks[Random.Range (0, spawnableClocks.Length)]);  position?
	}
	
	public void AddLife () {
		lives++;
	}

	public static void LoseLife () {
		lives--;
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
		//pauseMenu.SetActive(true);
	}

	public void Unpause () {
		//pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}
}
