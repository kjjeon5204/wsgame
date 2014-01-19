using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {

	public Texture2D black;
	public float alpha;
	private int drawDepth = -1000;
	public bool first = false;
	private IceBerg ice;
	// Use this for initialization
	void Start () {
		drawDepth = -1000;
		alpha = 1f;
	}
	
	// Update is called once per frame
	void OnGUI () {
		alpha -= 0.2f*Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color(0,0,0, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), black);
		if (alpha < 0.001f)
		{
			if (first)
			{
				ice = GetComponent<IceBerg>();
				ice.enabled = true;
			}
			alpha = 1f;
			this.enabled = false;
		}
	}
}
