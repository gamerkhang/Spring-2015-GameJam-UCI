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
	
	public Image HealthBar;
	public Image RightTreshold;
	public Image LeftTreshold;

	public Text TextClock;
	public Text TextThreshold;
	
	public float clickChangeRate;
	float timeChanger = 0f;
	
	float LifeMultiplier = 0f;

	float HealthAmount = 1f;


	bool isMouseOver = false;
	
	void Start () {
		successTickRate = GameManager.universalTickRate;
		
		currentTime = GameManager.CurrentUniversalTime + Random.Range(minTimeOffset, maxTimeOffset);
		timeToSuccess = Random.Range (minSuccessTime, maxSuccessTime);
		failureThreshold = Random.Range (minThreshold, maxThreshold);

		//TextThreshold.text = failureThreshold + " Min";
		
		//		InvokeRepeating ("UpdateClockTime", 0f, clockTickRate);
		//		InvokeRepeating ("UpdateTimeToSuccess", 0f, successTickRate);
		//		infoCard.gameObject.SetActive (false);
		LifeMultiplier = (float) 1 / timeToSuccess;
	}
	
	void DisplayTime(){
		hour = currentTime / 60;
		if (hour > 12) {
			hour -=(12*(hour/12));
		}
		min = currentTime % 60;
		TextClock.text = string.Format ("{0:D2}:{1:D2}", hour, min);
	}
	
	void Update(){
		if (Input.GetMouseButtonUp (0) || Input.GetMouseButtonUp (1)) {
			CursorChange.ChangeBack();
		}
	}

	void UpdateClockTime(){
		currentTime += 1;
		CheckFailure ();
	}


	void ControlTresholdBar(float fillingValue){
		int TresholdValue = currentTime - GameManager.getUniversalTime ();
		if (TresholdValue < 0) {
			LeftTreshold.color = (new Vector4(1f, 1f - fillingValue, 1f - fillingValue, 1f));
			LeftTreshold.fillAmount = fillingValue;
			RightTreshold.fillAmount = 0f;
		}
		else if (TresholdValue > 0) {
			RightTreshold.color = (new Vector4(1f, 1f - fillingValue, 1f - fillingValue, 1f));
			RightTreshold.fillAmount = fillingValue;
			LeftTreshold.fillAmount = 0f;
		}
	}


	void CheckFailure(){
        int absDifference = Mathf.Abs(GameManager.getUniversalTime() - currentTime);

        float redIntensity = (float)absDifference / failureThreshold;

		if(isMouseOver) 
			ControlTresholdBar (redIntensity);

        if (redIntensity >= 0.7f) //possibly change?
            this.GetComponent<RedOnWarning>().SetRed(redIntensity);
        else
            this.GetComponent<RedOnWarning>().ResetColor();

		if (absDifference >= failureThreshold)
		{
			GameManager.LoseLife();
			DisableClock();
		}


	}
	
	void UpdateTimeToSuccess()
	{
		HealthAmount -= LifeMultiplier;
		timeToSuccess--;

		
		if (timeToSuccess <= 0)
			DisableClock();
	}
	
	void OnMouseOver(){
		if (Time.timeScale == 0)
			return;

		HealthBar.fillAmount = HealthAmount;
		if (Input.GetMouseButton(0)) { //left button, speedup
			timeChanger += clickChangeRate;
			CursorChange.LeftClick();
			if( timeChanger > 1){
				currentTime += (int) timeChanger;
				timeChanger = 0f;
			}
            CheckFailure();
		}
		else if (Input.GetMouseButton(1)) { //right button, slowdown
			timeChanger += clickChangeRate;
			CursorChange.RightClick();
			if( timeChanger > 1){
				currentTime -= (int) timeChanger;
				timeChanger = 0f;
			}
            CheckFailure();
		}
		CheckFailure();
		DisplayTime();
	}
	
	void OnMouseEnter(){
		if (Time.timeScale == 0)
			return;
		
		HealthBar.fillAmount = HealthAmount;
		CheckFailure ();
		TextThreshold.text = failureThreshold + " Min";
		DisplayTime();
		isMouseOver = true;
		infoCard.gameObject.SetActive(true);
	}
	
	void OnMouseExit(){
		if (Time.timeScale == 0)
			return;
		isMouseOver = false;
		infoCard.gameObject.SetActive(false);
		CursorChange.ChangeBack();
	}
	
	void DisableClock()
	{
		GameManager.currentAmountClocks--;
		CancelInvoke("UpdateTimeToSuccess");
		CancelInvoke("UpdateClockTime");
		gameObject.SetActive(false);
		infoCard.gameObject.SetActive(false);
		CursorChange.ChangeBack();
        this.GetComponent<RedOnWarning>().ResetColor();
	}
	
	public void StartClock()
	{
		currentTime = GameManager.CurrentUniversalTime + Random.Range(minTimeOffset, maxTimeOffset);
		timeToSuccess = Random.Range(minSuccessTime, maxSuccessTime);
		failureThreshold = Random.Range(minThreshold, maxThreshold);
		
		LifeMultiplier = (float) 1 / timeToSuccess;
		HealthAmount = 1;
		InvokeRepeating("UpdateClockTime", 0f, clockTickRate);
		InvokeRepeating("UpdateTimeToSuccess", 0f, successTickRate);
	}
}