using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//Clock array
	public GameObject[] spawnableClocks;
	int lives = 5;
	int minClocks, maxClocks, gameHour, gameMinute, activeGameTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void AddClock () {
		//Instantiate(spawnableClocks[Random.Range (0, spawnableClocks.Length)]);  position?
	}

	public void LoseLife() {
		lives--;
		if (lives <= 0) {
			//lose;
		}
	}

	public void AddLife () {
		lives++;
	}
}
