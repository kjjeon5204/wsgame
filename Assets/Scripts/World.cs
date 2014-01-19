using UnityEngine;
using System.Collections;



public class World : MonoBehaviour {


	struct GameTracker {
		public int level;
		public int enemyCount;
		public GameObject[] enemy;
		public int typeAI;
	}

	GameTracker game;
	GameObject enemyTracker;
	public GameObject AI1;
	public GameObject AI2;
	private IceBerg player;

	public Texture2D day;
	public GUISkin UI;
	public GUISkin UI2;
	private Vector2 dayPOS;
	private Vector2 daySIZ;
	private Vector2 levelPOS;
	private Vector2 levelSIZ;
	private Vector2 offset;



	struct Coord{
		public float x;
		public float y;
	}
	
	//Coordinates bounded by the box
	Coord spawnRestrictionBox;


	public int get_level() {
		return game.level;
	}
	

	/*Randomly generates true or false;*/
	bool randomBoolGen () {
		int tracker = Random.Range (0, 3);
		if (tracker == 0) {
			return true;
		}
		else {
			return false;
		}
	}
	
	/*Effect: Generates random coordinate outside of given box*/
	Coord genCoordOut (Coord restrict){
		Coord retCoord;
		int axisDecider = Random.Range (0,4);
		if (axisDecider == 0) {
			retCoord.x = Random.Range (restrict.x + 10.0f, restrict.x + 20.0f);
			retCoord.y = Random.Range (-restrict.y, restrict.y);
		}
		else if (axisDecider == 1) {
			retCoord.x = Random.Range (-20.0f - restrict.x,-10.0f - restrict.x);
			retCoord.y = Random.Range (-restrict.y, restrict.y);
		}
		else if (axisDecider == 2) {
			retCoord.y = Random.Range (restrict.y + 10.0f, restrict.y + 20.0f);
			retCoord.x = Random.Range (-restrict.x, restrict.x);
		}
		else  {
			retCoord.y = Random.Range (-20.0f - restrict.y, -10.0f - restrict.y);
			retCoord.x = Random.Range (-restrict.x, restrict.x);
		}
		return retCoord;
	}
	
	public void startLevel () {
		game.enemyCount = 42;
		int genAISelector;
		game.enemy = new GameObject[game.enemyCount];
		Coord temp;
		Vector3 spawnPos;
		for (int ctr = 0; ctr < game.enemyCount; ctr ++) {
			genAISelector = Random.Range(0, game.typeAI);
			temp = genCoordOut (spawnRestrictionBox);
			spawnPos = new Vector3 (temp.x, 1f, temp.y);
			if (genAISelector == 0)
				game.enemy[ctr] = (GameObject)Instantiate (AI1, spawnPos, Quaternion.identity);
			if (genAISelector == 1)
				game.enemy[ctr] = (GameObject)Instantiate (AI2, spawnPos, Quaternion.identity);
		}
		game.level ++;
	}

	/*Game run functions*/
	public void object_is_destroyed() {
		game.enemyCount--;
	}

	void destroy_all_ai() {
		for (int ctr = 0; ctr < 5.0f; ctr ++) {
			if (game.enemy[ctr] != null) {
				Destroy(game.enemy[ctr]);
			}
		}
	}

	// Use this for initialization
	void Start () {
		game.level = 0;
		game.typeAI = 2;

		spawnRestrictionBox.x = 135.0f;
		spawnRestrictionBox.y = 57.0f;

		player = GetComponent<IceBerg>();
		startLevel();

		dayPOS.x = 0;
		daySIZ.x = 592f;
		daySIZ.y = 86f;
		dayPOS.y = Screen.height-daySIZ.y;
		levelPOS.y = Screen.height-86f+5f;
		levelPOS.x = 310f;
		levelSIZ.x = 200f;
		levelSIZ.y = 80f;
		offset.x = 3f;
		offset.y = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		if (game.enemyCount == 0 && !player.wave_over) {
			player.WinWave();
		}
	}
	void OnGUI() {
		if (!player.wave_over)
		{
			dayPOS.y = Screen.height-daySIZ.y;
			levelPOS.y = dayPOS.y+5f;
			GUI.skin = UI2;
			GUI.DrawTexture(new Rect(dayPOS.x, dayPOS.y, daySIZ.x, daySIZ.y), day);
			GUI.Label (new Rect(levelPOS.x+offset.x, levelPOS.y+offset.y, levelSIZ.x, levelSIZ.y), game.level.ToString());
			GUI.skin  = UI;
			GUI.Label (new Rect(levelPOS.x, levelPOS.y, levelSIZ.x, levelSIZ.y), game.level.ToString());
		}
	}
}
