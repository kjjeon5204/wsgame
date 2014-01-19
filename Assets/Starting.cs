using UnityEngine;
using System.Collections;

public class Starting : MonoBehaviour {

	public Vector2 startPOS;
	public Vector2 startSIZ;
	public Texture2D startBUT;
	public Vector2 quitPOS;
	public Vector2 quitSIZ;
	public Texture2D quitBUT;
	public Texture2D logo;
	public Vector2 logoPOS;
	public Vector2 logoSIZ;

	public Texture2D instruction;
	public Vector2 instPOS;
	public Vector2 instSIZ;

	public GUIStyle style;

	private FadeIn fader;

	void Start() {
		startSIZ.x = 246f;
		startSIZ.y = 93f;
		quitSIZ.x = 215f;
		quitSIZ.y = 100f;
		startPOS.x = Screen.width*0.75f-(startSIZ.x*0.5f);
		startPOS.y = Screen.height-startSIZ.y-quitSIZ.y-40f;
		quitPOS.x = Screen.width*0.75f-(quitSIZ.x*0.5f);
		quitPOS.y = startSIZ.y+startPOS.y;

		logoSIZ.x = 1024f;
		logoSIZ.y = 358f;
		logoPOS.x = Screen.width*0.5f-(logoSIZ.x*0.5f);
		logoPOS.y = 50f;

		instSIZ.x = Screen.width*0.34f;
		instSIZ.y = 200f;
		instPOS.y = Screen.height-instSIZ.y-40f;
		instPOS.x = Screen.width*0.25f-(instSIZ.x*0.5f);

		fader = GetComponent<FadeIn>();

	}

	void OnGUI() {
		style = new GUIStyle();
		GUI.DrawTexture(new Rect(instPOS.x, instPOS.y, instSIZ.x, instSIZ.y), instruction);
		GUI.DrawTexture(new Rect(logoPOS.x, logoPOS.y, logoSIZ.x, logoSIZ.y), logo);
		if(GUI.Button(new Rect(startPOS.x, startPOS.y, startSIZ.x, startSIZ.y), startBUT, style))
		{
			fader.enabled = true;
		}
		if(GUI.Button(new Rect(quitPOS.x, quitPOS.y, quitSIZ.x, quitSIZ.y), quitBUT, style))
		{
			Application.Quit();
		}
		if (fader.alpha > 0.99f)
			Application.LoadLevel(1);
	}
}
