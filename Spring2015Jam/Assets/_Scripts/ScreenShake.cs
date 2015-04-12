using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour 
{
	Vector3 cameraPos;
	public Camera myCamera; // set this via inspector
	public static float shake = 0;
	public float shakeAmount = 0.7f;
	public float decreaseFactor  = 1.0f;

	void Start(){
		cameraPos = myCamera.transform.localPosition;

	}
	void Update() {
		if (shake > 0) {

			myCamera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
			myCamera.transform.localPosition = new Vector3(myCamera.transform.localPosition.x, myCamera.transform.localPosition.y, -10.0f);
			shake -= Time.deltaTime * decreaseFactor;
			Debug.Log(shake);
			
		} else {
			myCamera.transform.localPosition = cameraPos;
			shake = 0.0f;
		}
	}
}
