using UnityEngine;
using System.Collections;

public class IceBerg : MonoBehaviour {
	private float health;
	private float curhealth;
	private int pieces;
	private float pieceHP;

	private float hplength;
	private float hplengthMAX;
	private Vector2 hpPOS;
	private float hpheight;
	private Vector2 frame;
	public Texture2D Bar;
	public Texture2D Frame;
	public Texture2D hungry;
	private Vector2 hungrySIZ;
	private Vector2 hungryPOS;

	private GameObject[] ice;

//	private float penguinSIZ;
	public Texture2D crosshair;
	private bool[] penguins;
	private int index;
	private bool won_wave;
	private bool degrade;
	private bool pause;
	private bool fade1;
	private bool fade2;
	public bool wave_over;
	private bool soundFX;

	private Sound music;
	private World world;
	private Movement move;
	private FadeIn fadeI;
	private FadeOut fadeO;
	public GUIStyle style;
	public float speed;

	public bool LOST;
	public GUISkin UI;
	public GUISkin UI2;
	public Vector2 levelPOS;
	public Vector2 levelSIZ;
	private Vector2 offset;
//	public Vector2 penguin1;
//	public Vector2 penguin2;
//	public Vector2 penguin3;
//	public Vector2 penguin4;
//	public Vector2 penguin5;
//	public Vector2 penguin6;
//	public Vector2 penguin7;
//	public Vector2 penguin8;
//	public Vector2 penguin9;
//	public Vector2 penguin10;
//	public Vector2 penguin11;

	private float interval;
	private float counter;

	protected GameObject cam;
	public float scale;

	// Use this for initialization
	void Start () {
		LOST = false;
		counter = 0;
		soundFX = false;
		audio.Play();
		pieces = 12;
		pieceHP = 100f;
		scale = 0.635f;
		hplengthMAX = scale*Screen.width;
		hplength = hplengthMAX;
		frame.x = 0.66f*Screen.width;
		frame.y = 100f;
		hpPOS.x = 15;
		hpPOS.y = 12;
		hpheight = 40;

		hungrySIZ.x = 650f;
		hungrySIZ.y = 100f;
		hungryPOS.x = (Screen.width-hungrySIZ.x)*0.5f;
		hungryPOS.y = 20f;

		ice = new GameObject[11];
		ice[0] = transform.Find("iceberg_1").gameObject;
		ice[1] = transform.Find("iceberg_2").gameObject;
		ice[2] = transform.Find("iceberg_3").gameObject;
		ice[3] = transform.Find("iceberg_4").gameObject;
		ice[4] = transform.Find("iceberg_5").gameObject;
		ice[5] = transform.Find("iceberg_6").gameObject;
		ice[6] = transform.Find("iceberg_7").gameObject;
		ice[7] = transform.Find("iceberg_8").gameObject;
		ice[8] = transform.Find("iceberg_9").gameObject;
		ice[9] = transform.Find("iceberg_10").gameObject;
		ice[10] = transform.Find("iceberg_11").gameObject;

//		penguinSIZ = 30f;
		penguins = new bool[11];
		won_wave = false;
		degrade = false;
		wave_over = false;
		index = -1;
		speed = 4f;
		pause = false;

		for (int i = 0; i < 11; i++)
		{
			penguins[i] = true;
		}
//		penguin1.x = 5f;
//		penguin1.y = -88f;
//		penguin2.x = 35f;
//		penguin2.y = -53f;
//		penguin3.x = 47f;
//		penguin3.y = -15f;
//		penguin4.x = 48f;
//		penguin4.y = 43f;
//		penguin5.x = 10f;
//		penguin5.y = 63f;
//		penguin6.x = -28f;
//		penguin6.y = 78f;
//		penguin7.x = -62f;
//		penguin7.y = 58f;
//		penguin8.x = -93f;
//		penguin8.y = 25f;
//		penguin9.x = -90f;
//		penguin9.y = -26f;
//		penguin10.x = -84f;
//		penguin10.y = -66f;
//		penguin11.x = -41f;
//		penguin11.y = -87f;

		move = GetComponent<Movement>();
		world = GetComponent<World>();
		fadeI = GetComponent<FadeIn>();
		fadeO = GetComponent<FadeOut>();
		music = GetComponent<Sound>();
		fade1 = false;
		fade2 = false;
		world.enabled = true;
		
		levelSIZ.x = 600f;
		levelSIZ.y = 80f;
		levelPOS.y = Screen.height-86f+5f;
		levelPOS.x = Screen.height*0.75f;
		offset.x = 3f;
		offset.y = 3f;

		cam = GameObject.Find("Main Camera");
		Heal();
	}

	public void Heal() {
		health = pieces*pieceHP;
		curhealth = health;
		move.speedUP();
	}

	public void subtractHealth(float change) {
		curhealth = curhealth - change;
		if (curhealth < 0f)
			curhealth = 0f;
	}

	public void WinWave() {
		won_wave = true;
		wave_over = true;
		if (pieces == 1)
		{
			wave_over = false;
			degrade = false;
			won_wave = false;
			pause = true;
		}
		else
			transform.position = new Vector3(0,1,0);
			
	}

	void Lose() {
		audio.Stop();
		LOST = true;
		transform.position = new Vector3(0,1,0);
		GameObject[] names = GameObject.FindGameObjectsWithTag("Animal");
		foreach (Object animal in names)
		{
			Destroy(animal);
		}
		GameObject test;
		test = GameObject.Find("Main Camera");
		test.camera.enabled = false;
		test = GameObject.Find("Lose");
		test.camera.enabled = true;
	}


	// Update is called once per frame
	void Update () {
		if (soundFX)
		{
			counter += Time.deltaTime;
			if (counter >= 1.5f)
			{
				soundFX = false;
				audio.Play();
				counter = 0;
			}
		}
		if (index >= 0)
		{
			if (fade1)
			{
				if (fadeI.alpha > 0.7f && !soundFX)
				{
					audio.Pause();
					music.Bear();
					soundFX = true;
				}
				if (fadeI.alpha > 0.9f)
				{
					fade2 = true;
					fadeO.enabled = true;
					fade1 = false;
					Destroy (ice[index].transform.FindChild("penguin").gameObject);
				}
			}
			else if (fade2 && fadeO.alpha < 0.1f)
			{
				degrade = true;
				fade2 = false;
			}
		}
		if (degrade)
			LosePiece(index);
		if (pause)
		{
			Interval(5f);
		}
		if (curhealth == 0f)
			Lose();
	}

	void LosePiece(int ind) {
		Vector3 movement = ice[ind].transform.position - transform.position;
		movement = Vector3.Normalize(movement);
		movement += new Vector3(0, -0.35f, 0);
		ice[ind].transform.Translate(movement*Time.deltaTime*speed);
		if (ice[ind].transform.position.y <= -5f)
		{
			Destroy(ice[ind]);
			index = -1;
			move.enabled = true;
			pause = true;
			degrade = false;
			Heal();
			cam.transform.position = new Vector3(0, 175f, 0);
		}
	}

	void Interval(float t) {
		interval += Time.deltaTime;
		if (interval >= t)
		{
			world.startLevel ();
			wave_over = false;
			interval = 0;
			pause = false;
			music.Horn();
		}
	}

	void OnGUI() {
		style = new GUIStyle();
		if (!LOST)
		{
			hplengthMAX = scale*Screen.width;
			frame.x = 0.66f*Screen.width;
			hplength = curhealth/health*hplengthMAX;

			if (!won_wave && index == -1)
			{
				GUI.DrawTexture(new Rect(hpPOS.x,hpPOS.y,hplength,hpheight), Bar);
				GUI.DrawTexture(new Rect(0,0,frame.x,frame.y), Frame);
			}


			if (won_wave && pieces > 1)
			{
				GUI.DrawTexture(new Rect(hungryPOS.x, hungryPOS.y, hungrySIZ.x, hungrySIZ.y), hungry);
				cam.transform.position = new Vector3(0, 38.5f, 0);
				move.enabled = false;


				if (Input.GetKey(KeyCode.E))
				{
					if (penguins[0])
						//if (GUI.Button(new Rect(Screen.width*0.5f+penguin1.x,Screen.height*0.5f+penguin1.y, penguinSIZ, penguinSIZ), crosshair, style))
					{
						penguins[0] = false;
						index = 0;
						pieces -= 1;
					}
					else if (penguins[1])
						//if (GUI.Button(new Rect(Screen.width*0.5f+penguin2.x,Screen.height*0.5f+penguin2.y, penguinSIZ, penguinSIZ), crosshair, style))
					{
						penguins[1] = false;
						index = 1;
						pieces -= 1;
					}
					else if (penguins[2])
						//if (GUI.Button(new Rect(Screen.width*0.5f+penguin3.x,Screen.height*0.5f+penguin3.y, penguinSIZ, penguinSIZ), crosshair, style))
					{
						penguins[2] = false;
						index = 2;
						pieces -= 1;
					}
					else if (penguins[3])
						//if (GUI.Button(new Rect(Screen.width*0.5f+penguin4.x,Screen.height*0.5f+penguin4.y, penguinSIZ, penguinSIZ), crosshair, style))
					{
						penguins[3] = false;
						index = 3;
						pieces -= 1;
					}
					else if (penguins[4])
						//if (GUI.Button(new Rect(Screen.width*0.5f+penguin5.x,Screen.height*0.5f+penguin5.y, penguinSIZ, penguinSIZ), crosshair, style))
					{
						penguins[4] = false;
						index = 4;
						pieces -= 1;
					}
					else if (penguins[5])
						//if (GUI.Button(new Rect(Screen.width*0.5f+penguin6.x,Screen.height*0.5f+penguin6.y, penguinSIZ, penguinSIZ), crosshair, style))
					{
						penguins[5] = false;
						index = 5;
						pieces -= 1;
					}
					else if (penguins[6])
						//if (GUI.Button(new Rect(Screen.width*0.5f+penguin7.x,Screen.height*0.5f+penguin7.y, penguinSIZ, penguinSIZ), crosshair, style))
					{
						penguins[6] = false;
						index = 6;
						pieces -= 1;
					}
					else if (penguins[7])
						//if (GUI.Button(new Rect(Screen.width*0.5f+penguin8.x,Screen.height*0.5f+penguin8.y, penguinSIZ, penguinSIZ), crosshair, style))
					{
						penguins[7] = false;
						index = 7;
						pieces -= 1;
					}
					else if (penguins[8])
						//if (GUI.Button(new Rect(Screen.width*0.5f+penguin9.x,Screen.height*0.5f+penguin9.y, penguinSIZ, penguinSIZ), crosshair, style))
					{
						penguins[8] = false;
						index = 8;
						pieces -= 1;
					}
					else if (penguins[9])
						//if (GUI.Button(new Rect(Screen.width*0.5f+penguin10.x,Screen.height*0.5f+penguin10.y, penguinSIZ, penguinSIZ), crosshair, style))
					{
						penguins[9] = false;
						index = 9;
						pieces -= 1;
					}
					else if (penguins[10])
						//if (GUI.Button(new Rect(Screen.width*0.5f+penguin11.x,Screen.height*0.5f+penguin11.y, penguinSIZ, penguinSIZ), crosshair, style))
					{
						penguins[10] = false;
						index = 10;
						pieces -= 1;
					}
					if (index != -1)
					{
						won_wave = false;
						fade1 = true;
						fadeI.enabled = true;
					}

				}
			}
		}//!LOST statement
		else
		{
			GUI.skin = UI2;
			GUI.Label (new Rect(levelPOS.x+offset.x, levelPOS.y+offset.y, levelSIZ.x, levelSIZ.y),"Survived");
			GUI.skin  = UI;
			GUI.Label (new Rect(levelPOS.x, levelPOS.y, levelSIZ.x, levelSIZ.y), "Survived");
		}
	}
}
