using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Tutorial : MonoBehaviour {
	public Image LeftClick;
	public Image RightClick;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver(){
		if (Time.timeScale == 0)
			return;

		if (Input.GetMouseButton (0)) { //left button, speedup
			LeftClick.gameObject.SetActive(true);
			RightClick.gameObject.SetActive(false);
		} else if (Input.GetMouseButton (1)) { //right button, slowdown
			LeftClick.gameObject.SetActive(false);
			RightClick.gameObject.SetActive(true);
		}

	}
}
