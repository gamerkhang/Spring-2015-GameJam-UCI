using UnityEngine;
using System.Collections;

public class ExplosionHandlerScript : MonoBehaviour {

    public void playExplosionSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
