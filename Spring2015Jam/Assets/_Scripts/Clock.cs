using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Clock : MonoBehaviour
{
    public GameManager manager;
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
    public Image RightThreshold;
    public Image LeftThreshold;

    public Text TextClock;

    public float clickChangeRate;
    float timeChanger = 0f;

    float LifeMultiplier = 0f;

    float HealthAmount = 1f;


    bool isMouseOver = false;

    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        successTickRate = GameManager.universalTickRate;

        currentTime = manager.CurrentUniversalTime + Random.Range(minTimeOffset, maxTimeOffset);
        timeToSuccess = Random.Range(minSuccessTime, maxSuccessTime);
        failureThreshold = Random.Range(minThreshold, maxThreshold);

        //		InvokeRepeating ("UpdateClockTime", 0f, clockTickRate);
        //		InvokeRepeating ("UpdateTimeToSuccess", 0f, successTickRate);
        //		infoCard.gameObject.SetActive (false);
        LifeMultiplier = (float)1 / timeToSuccess;
    }

    void DisplayTime()
    {
        hour = currentTime / 60;
        if (hour > 12)
        {
            hour -= (12 * (hour / 12));
        }
        min = currentTime % 60;
        TextClock.text = string.Format("{0:D2}:{1:D2}", hour, min);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            CursorChange.ChangeBack();
        }
    }

    void UpdateClockTime()
    {
        if (positiveRange)
        {
            currentTime += 1;
        }
        else
        {
            currentTime -= 1;
        }
        CheckFailure();
    }

    void ControlThresholdBar(float fillingValue)
    {
        int ThresholdValue = currentTime - manager.CurrentUniversalTime;
        if (ThresholdValue < 0)
        {
            LeftThreshold.color = (new Vector4(1f, 1f - fillingValue, 1f - fillingValue, 1f));
            LeftThreshold.fillAmount = fillingValue;
            RightThreshold.fillAmount = 0f;
        }
        else if (ThresholdValue > 0)
        {
            RightThreshold.color = (new Vector4(1f, 1f - fillingValue, 1f - fillingValue, 1f));
            RightThreshold.fillAmount = fillingValue;
            LeftThreshold.fillAmount = 0f;
        }
    }

    void CheckFailure()
    {
        int absDifference = Mathf.Abs(manager.CurrentUniversalTime - currentTime);

        float redIntensity = (float)absDifference / failureThreshold;

        if (isMouseOver)
            ControlThresholdBar(redIntensity);

        if (redIntensity >= 0.7f) //possibly change?
            this.GetComponent<RedOnWarning>().SetRed(redIntensity);
        else
            this.GetComponent<RedOnWarning>().ResetColor();

        if (absDifference >= failureThreshold)
        {
            manager.LoseLife();
            DisableClock();
        }
    }

    void UpdateTimeToSuccess()
    {
        HealthAmount -= LifeMultiplier;
        timeToSuccess--;

        if (timeToSuccess <= 0)
        {
            manager.AddtoStreak();
            DisableClock();
        }
    }

    void OnMouseOver()
    {
        if (Time.timeScale == 0)
            return;

        HealthBar.fillAmount = HealthAmount;
        if (Input.GetMouseButton(0))
        { //left button, speedup
            timeChanger += clickChangeRate;
            CursorChange.LeftClick();
            if (timeChanger > 1)
            {
                currentTime += (int)timeChanger;
                timeChanger = 0f;
            }
        }
        else if (Input.GetMouseButton(1))
        { //right button, slowdown
            timeChanger += clickChangeRate;
            CursorChange.RightClick();
            if (timeChanger > 1)
            {
                currentTime -= (int)timeChanger;
                timeChanger = 0f;
            }
        }
        if (infoCard.gameObject.activeInHierarchy == true)
        {
            infoCard.gameObject.SetActive(true);
        }

        CheckFailure();
        DisplayTime();
    }

    void OnMouseEnter()
    {
        if (Time.timeScale == 0)
            return;

        HealthBar.fillAmount = HealthAmount;
        CheckFailure();
        DisplayTime();
        isMouseOver = true;
        infoCard.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        if (Time.timeScale == 0)
            return;
        isMouseOver = false;
        infoCard.gameObject.SetActive(false);
        CursorChange.ChangeBack();
    }

    void DisableClock()
    {
        manager.RemoveClock();
        CancelInvoke("UpdateTimeToSuccess");
        CancelInvoke("UpdateClockTime");
        infoCard.gameObject.SetActive(false);
        CursorChange.ChangeBack();
        this.GetComponent<RedOnWarning>().ResetColor();
        gameObject.SetActive(false);
    }

    public void StartClock()
    {
        currentTime = manager.CurrentUniversalTime + Random.Range(minTimeOffset, maxTimeOffset);
        timeToSuccess = Random.Range(minSuccessTime, maxSuccessTime);
        failureThreshold = Random.Range(minThreshold, maxThreshold);

        LifeMultiplier = (float)1 / timeToSuccess;
        HealthAmount = 1;
        InvokeRepeating("UpdateClockTime", 0f, clockTickRate);
        InvokeRepeating("UpdateTimeToSuccess", 0f, successTickRate);
    }
}