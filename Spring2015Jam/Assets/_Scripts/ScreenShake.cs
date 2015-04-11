using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour 
{
	public static CameraShake Instance;

	private float _amplitude = 0.1;
	private float _duration = 0.5f;

	private Vector3 initialPosition;
	private bool isShaking = false;

	// Use this for initialization
	void Start () 
	{
		Instance = this;
		initialPosition = transform.localPosition;
	}

	public void Shake(float amplitude, float duration)
	{
		isShaking = true
		CancelInvoke();
		Invoke("StopShaking", duration);
	}

	public void StopShaking()
	{
		isShaking = false
	}
	// Update is called once per frame
	void Update () 
	{
		if(isShaking)
		{
			transform.localPosition = initialPosition + Random.insideUnitSphere+amplitude;
		}
	}
}
