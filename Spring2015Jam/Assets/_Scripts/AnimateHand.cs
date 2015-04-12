using UnityEngine;
using System.Collections;

public class AnimateHand : MonoBehaviour {
	public float speed = 5.0f;

	private float timeElapsed = 0.0f;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Animate", 5, 1F);

	}
	
	// Update is called once per frame
	void Animate () {
		timeElapsed += Time.deltaTime;
		transform.eulerAngles = new Vector3(0.0f, 0.0f, timeElapsed * speed);
	}
}
