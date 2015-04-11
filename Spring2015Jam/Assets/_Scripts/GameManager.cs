using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//Clock array
	public GameObject[] spawnableClocks;
	public int gameHour = 12, gameMinute = 0;
	int lives = 5;
	bool gameRunning = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (gameRunning) {

		}
	}

	void FixedUpdate () {
		if (gameRunning) {
			//Universal clock to sync w/


		}
	}

	void AddClock () {
		//Instantiate(spawnableClocks[Random.Range (0, spawnableClocks.Length)]);  position?
	}

	public void LoseLife() {
		lives--;
		if (lives <= 0) {
			Time.timeScale = 0;
			//make button to try again or quit active; try again button reloads the scene.
		}
	}

	public void AddLife () {
		lives++;
	}
}
