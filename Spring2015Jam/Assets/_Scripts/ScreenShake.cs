using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour 
{
	public Camera myCamera; // set this via inspector
	public float shake = 0;
	public float shakeAmount = 0.7f;
	public float decreaseFactor  = 1.0f;
	
	void Update() {
		if (shake > 0) {
			myCamera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
			myCamera.transform.localPosition = new Vector3(myCamera.transform.localPosition.x, myCamera.transform.localPosition.y, -10.0f);
			shake -= Time.deltaTime * decreaseFactor;
			Debug.Log(shake);
			
		} else {
			shake = 0.0f;
		}
	}
}
