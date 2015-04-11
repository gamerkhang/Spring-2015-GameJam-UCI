using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Clock : MonoBehaviour {
	public int timeToSuccess, failureThreshold;

	public float successTickRate;
	public float clockTickRate;

	public GameObject infoCard;

    public int minTimeOffset, maxTimeOffset;
	public int minSuccessTime, maxSuccessTime;
	public int minThreshold, maxThreshold;

	public int hour, min, currentTime;

	public bool ClockAlive = true;

	public bool positiveRange = true;
	
    public Image ClockHealthBar;
	public Text TextClock;
	public Text TextThreshold;


	float timeChanger = 0f;
	float LifeMultiplier = 0f;

	void Start () {
		successTickRate = GameManager.universalTickRate;

        currentTime = GameManager.CurrentUniversalTime + Random.Range(minTimeOffset, maxTimeOffset);
		timeToSuccess = Random.Range (minSuccessTime, maxSuccessTime);
		failureThreshold = Random.Range (minThreshold, maxThreshold);

        //SliderSuccess.maxValue = timeToSuccess;
        TextThreshold.text = failureThreshold + " Min";

		InvokeRepeating ("UpdateClockTime", 0f, clockTickRate);
		InvokeRepeating ("UpdateTimeToSuccess", 0f, successTickRate);
		infoCard.gameObject.SetActive (false);

		LifeMultiplier = (float) 1 / timeToSuccess;
	}

	void ParseTime(Text t, int minutes){
		hour = minutes / 60;
		if (hour > 12) {
			hour -=(12*(hour/12));
		}
		min = minutes % 60;
		t.text = string.Format ("{0:D2}:{1:D2}", hour, min);
	}


	void UpdateClockTime(){
		currentTime += 1;
		CheckLimits ();
		ParseTime (TextClock, currentTime);
	}

	void CheckLimits(){
        if (Mathf.Abs(GameManager.getUniversalTime() - currentTime) >= failureThreshold)
        {
            GameManager.LoseLife();
            DisableClock();
        };
	}

    void UpdateTimeToSuccess()
    {
		timeToSuccess--;
		ClockHealthBar.fillAmount -= LifeMultiplier;

        if (timeToSuccess <= 0)
            DisableClock();
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

    void DisableClock()
    {
        CancelInvoke("UpdateTimeToSuccess");
        CancelInvoke("UpdateClockTime");
        gameObject.SetActive(false);
		infoCard.gameObject.SetActive(false);
    }

    void StartClock()
    {
        currentTime = GameManager.CurrentUniversalTime + Random.Range(minTimeOffset, maxTimeOffset);
        timeToSuccess = Random.Range(minSuccessTime, maxSuccessTime);
        failureThreshold = Random.Range(minThreshold, maxThreshold);

//        SliderSuccess.maxValue = timeToSuccess;
        TextThreshold.text = failureThreshold + " Min";

        InvokeRepeating("UpdateClockTime", 0f, clockTickRate);
        InvokeRepeating("UpdateTimeToSuccess", 0f, successTickRate);
    }
}
