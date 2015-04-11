using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Clock : MonoBehaviour {
	int lifeTime, killRange;
	float rate;
	GameObject GameManager;
	public GameObject infoCard;
	public int maxLife;
	public int minLife;
	public int maxRange;
	public int minRange;
	int hour, min, currentTime;
	float delay = 0f;
	public bool positiveRange = true;
	float timeChanger = 0f;
	public Text ClockTime;


	void Start () {
		GameManager = GameObject.Find ("GameManager");
		lifeTime = Random.Range (minLife, maxLife);
		killRange = Random.Range (minRange, maxRange);
		currentTime = 770;
		InvokeRepeating ("UpdateClockTime", 1.0f, 0.2f);
		infoCard.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		delay += 5 * Time.deltaTime;
//		if (delay >= 2) {
//			delay = 0;
//
////			if (min > 59) {
////				min = 0;
////				lifeTime-=1;
////				hour += 1;
////				
////			}
//			CheckLimits();
//			printTime ();
//		}


	}
	void CheckLimits(){
		if (lifeTime <= 0)
			Debug.Log ("im deaD");
		//if (game_hour <= currentTime+killRange && positiveRange)
		//	Debug.Log ("explosion");
	}

	void UpdateClockTime(){
		currentTime += 1;
		lifeTime -= 1;
		hour = currentTime / 60;
		if (hour > 12) {
			hour = hour-(12*(hour/12));
		}
		min = currentTime % 60;
		CheckLimits ();
		printTime ();
	}

	void printTime(){
		string minute="";
		if(min<10)
			minute = "0";
		ClockTime.text = (hour + ":"+minute + min).ToString();
	}

	void OnMouseOver(){
		if (Input.GetMouseButton(0)) { //left button
			timeChanger += .25f;
			if( timeChanger > 1){
				currentTime += (int) timeChanger;
				timeChanger = 0f;
			}
			//Debug.Log("here");
		}
		else if (Input.GetMouseButton(1)) { //right button
			timeChanger += .25f;
			if( timeChanger > 1){
				currentTime -= (int) timeChanger;
				timeChanger = 0f;
			}
		}
	}

	void OnMouseEnter(){
		infoCard.gameObject.SetActive(true);
	}

	void OnMouseExit(){
		infoCard.gameObject.SetActive(false);
	}
}
