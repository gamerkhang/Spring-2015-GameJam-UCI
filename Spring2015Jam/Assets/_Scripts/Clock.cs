using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Clock : MonoBehaviour {
	int lifeTime, killRange;

	public float changing_rate;

	public GameObject infoCard;

	public int maxLife;
	public int minLife;
	public int maxRange;
	public int minRange;

	int hour, min, currentTime;

	bool ClockAlive = true;

	public bool positiveRange = true;
	
	float timeChanger = 0f;

	public Text ClockLife;
	public Text ClockTime;
	public Text ClockRange;
	public Scrollbar slid;

	float multiplier = 0f;

	void Start () {

		killRange = Random.Range (minRange, maxRange);
		currentTime = 725;
		lifeTime = Random.Range (minLife, maxLife);
		setClockLife ();
		setKillRange ();
		hour = min = 0;
		InvokeRepeating ("UpdateClockTime", 0f, changing_rate);
		InvokeRepeating ("LowerLifeTime", 0f, changing_rate);
		infoCard.gameObject.SetActive (false);
		multiplier = (float)1/lifeTime;

	}

	void setClockLife (){
		ParseTime (ClockLife, lifeTime);
	}

	void setKillRange (){
		ClockRange.text = killRange + "Min";
	}

	void ParseTime(Text t, int minutes){
		hour = minutes / 60;
		if (hour > 12) {
			hour -=(12*(hour/12));
		}
		min = minutes % 60;
		string minute="";
		if(min<10)
			minute = "0";
		t.text = string.Format ("{0:D2}:{1:D2}", hour, min);
	}


	void UpdateClockTime(){
		currentTime += 1;
		CheckLimits ();
		ParseTime (ClockTime, currentTime);
	}

	void CheckLimits(){
		if (lifeTime <= 0 && ClockAlive) {
			lifeTime=1;
			currentTime = 0;
			hour = 0;
		} else if (positiveRange) {
			if ((currentTime - GameManager.getUniversalTime ()) >= killRange) {
				GameManager.LoseLife ();
				ClockAlive=false;
				killRange = 999;
				setKillRange ();
			}
		} else if ((GameManager.getUniversalTime () - currentTime) >= killRange) {
			GameManager.LoseLife ();
			ClockAlive=false;
			killRange = 999;
			setKillRange ();
		}
	}

	void LowerLifeTime (){
		if (ClockAlive) {
			lifeTime--;
			slid.size -= multiplier;
			setClockLife ();
		}
	}

	void OnMouseOver(){
		if (Input.GetMouseButton(0)) { //left button
			timeChanger += .25f;
			if( timeChanger > 1){
				currentTime += (int) timeChanger;
				timeChanger = 0f;
			}
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
