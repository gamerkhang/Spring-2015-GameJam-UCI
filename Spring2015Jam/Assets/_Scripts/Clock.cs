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
	public Text TextClock;
	public Text TextThreshold;
	
	public float clickChangeRate;
	float timeChanger = 0f;
	
	float LifeMultiplier = 0f;
	float HealthAmount = 1f;
	
	void Start () {
		successTickRate = GameManager.universalTickRate;
		
		currentTime = GameManager.CurrentUniversalTime + Random.Range(minTimeOffset, maxTimeOffset);
		timeToSuccess = Random.Range (minSuccessTime, maxSuccessTime);
		failureThreshold = Random.Range (minThreshold, maxThreshold);
		
		//SliderSuccess.maxValue = timeToSuccess;
		TextThreshold.text = failureThreshold + " Min";
		
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
	
	void CheckFailure(){
        int absDifference = Mathf.Abs(GameManager.getUniversalTime() - currentTime);
        float redIntensity = (float)absDifference / failureThreshold;
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
		DisplayTime();
	}
	
	void OnMouseEnter(){
		if (Time.timeScale == 0)
			return;
		
		HealthBar.fillAmount = HealthAmount;
		TextThreshold.text = failureThreshold + " Min";
		DisplayTime();
		infoCard.gameObject.SetActive(true);
	}
	
	void OnMouseExit(){
		if (Time.timeScale == 0)
			return;
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