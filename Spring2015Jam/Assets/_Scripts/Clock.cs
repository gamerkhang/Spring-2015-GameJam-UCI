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
	
	
	void UpdateClockTime(){
		currentTime += 1;
		CheckFailure ();
		DisplayTime ();
	}
	
	void CheckFailure(){
		if (Mathf.Abs(GameManager.getUniversalTime() - currentTime) >= failureThreshold)
		{
			GameManager.LoseLife();
			DisableClock();
		}
	}
	
	void UpdateTimeToSuccess()
	{
		timeToSuccess--;
		HealthBar.fillAmount -= LifeMultiplier;
		
		if (timeToSuccess <= 0)
			DisableClock();
	}
	
	void OnMouseOver(){        
		if (Input.GetMouseButton(0)) { //left button, speedup
			timeChanger += clickChangeRate;
			if( timeChanger > 1){
				currentTime += (int) timeChanger;
				timeChanger = 0f;
			}
			DisplayTime();
		}
		else if (Input.GetMouseButton(1)) { //right button, slowdown
			timeChanger += clickChangeRate;
			if( timeChanger > 1){
				currentTime -= (int) timeChanger;
				timeChanger = 0f;
			}
			DisplayTime();
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
		GameManager.currentAmountClocks--;
		CancelInvoke("UpdateTimeToSuccess");
		CancelInvoke("UpdateClockTime");
		gameObject.SetActive(false);
		infoCard.gameObject.SetActive(false);
	}
	
	public void StartClock()
	{
		currentTime = GameManager.CurrentUniversalTime + Random.Range(minTimeOffset, maxTimeOffset);
		timeToSuccess = Random.Range(minSuccessTime, maxSuccessTime);
		failureThreshold = Random.Range(minThreshold, maxThreshold);
		
		HealthBar.fillAmount = 1;
		LifeMultiplier = (float) 1 / timeToSuccess;
		TextThreshold.text = failureThreshold + " Min";
		
		InvokeRepeating("UpdateClockTime", 0f, clockTickRate);
		InvokeRepeating("UpdateTimeToSuccess", 0f, successTickRate);
	}
}