using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {

    public void ChangeToScene(string sceneToChangeTo)
    {
        Application.LoadLevel(sceneToChangeTo);
    }

	public void Quit()
	{
		Application.Quit();
	}
}
