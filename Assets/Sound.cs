using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	public AudioClip horn;
	public AudioClip bear;

	public void Horn() {
		audio.PlayOneShot(horn);
	}
	public void Bear() {
		audio.PlayOneShot(bear);
	}
}
