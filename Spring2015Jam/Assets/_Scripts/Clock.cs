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
	public Text ClockLife;
	public Text ClockTime;
	public Text ClockRange;
	public Scrollbar slid;
	void Start () {
		GameManager = GameObject.Find ("GameManager");
		killRange = Random.Range (minRange, maxRange);
		currentTime = 770;
		lifeTime = Random.Range (minLife, maxLife);
		setClockLife ();
		setKillRange ();
		hour = min = 0;
		InvokeRepeating ("UpdateClockTime", 1f, 0.2f);
		InvokeRepeating ("LowerLifeTime", 0f, 1f);
		infoCard.gameObject.SetActive (false);

	}
	

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
		if (lifeTime <= 0) {
			currentTime = 0;
			hour = 0;
		}
	}

	void setClockLife (){
		ParseTime (ClockLife, (currentTime + lifeTime));
	}

	void setKillRange (){
		ClockRange.text = killRange + "Min";
	}

	void ParseTime(Text t, int minutes){
		hour = minutes / 60;
		if (hour > 12) {
			hour -=(12*(hour/12));
		}
		min = currentTime % 60;
		string minute="";
		if(min<10)
			minute = "0";
		t.text = (hour + ":" + minute + min).ToString ();
	}


	void UpdateClockTime(){
		currentTime += 1;

		CheckLimits ();
		ParseTime (ClockTime, currentTime);
	}

	void LowerLifeTime (){
		lifeTime--;
		slid.size -= 0.2f;
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
