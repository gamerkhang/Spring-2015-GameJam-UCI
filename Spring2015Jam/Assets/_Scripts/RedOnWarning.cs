using UnityEngine;
using System.Collections;

public class RedOnWarning : MonoBehaviour {
    SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
        renderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	}  

    public void SetRed(float redIntensity)
    {
        renderer.material.SetColor("_Color", new Vector4(1f, 1f - redIntensity, 1f - redIntensity, 1f));
    }

    public void ResetColor()
    {
        renderer.material.SetColor("_Color", new Vector4(1f, 1f, 1f, 1f));
    }
}
