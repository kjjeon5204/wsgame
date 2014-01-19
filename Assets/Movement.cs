using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	private Vector2 pos;
	private float speed;
	public float boundaryX;
	public float boundaryZ;
	private Vector3 movement;

	private Vector2 crossPOS;
	private float crossSIZE;
	private Vector3 mouse;
	public Texture2D crosshair;

	private float fireRate;
	private float timeSince;
	private float bearRate;
	private float bearTime;
	public Transform bullet;
	public Transform ball;

	private GameObject[] muzzles;
	private GameObject cannon;

	// Use this for initialization
	void Start () {
		pos.x = transform.position.x;
		pos.y = transform.position.z;
		speed = 15f;
		boundaryX = 137f;
		boundaryZ = 57f;

		crossPOS.x = Screen.width*0.5f;
		crossPOS.y = Screen.height*0.5f;
		crossSIZE = 28f;
		Screen.showCursor = false;

		fireRate = 0.34f;
		timeSince = 0;
		bearRate = 0.5f;
		bearTime = 0;

		muzzles = GameObject.FindGameObjectsWithTag("muzzle");
		cannon = GameObject.FindGameObjectWithTag("muzzle2");
	}

	public void speedUP() {
		speed += 5f;
	}

	// Update is called once per frame
	void Update () {
		movement = Vector3.zero;
		if (pos.x < boundaryX && Input.GetKey(KeyCode.D))
		{
			movement = movement + Vector3.right;
		}
		else if (pos.x > -boundaryX && Input.GetKey(KeyCode.A))
		{
			movement = movement + Vector3.left;
		}
		if (pos.y < boundaryZ && Input.GetKey(KeyCode.W))
		{  
			movement = movement + Vector3.forward;
		}
		else if (pos.y > -boundaryZ && Input.GetKey(KeyCode.S))
		{
			movement = movement + Vector3.back;
		}
		movement = Vector3.Normalize(movement);
		transform.Translate(movement*Time.deltaTime*speed);
		pos.x = transform.position.x;
		pos.y = transform.position.z;


		if (timeSince < fireRate)
			timeSince += Time.deltaTime;
		if (bearTime < bearRate)
			bearTime += Time.deltaTime;
		
		if (Input.GetMouseButton(0))
		{
			if (fireRate  < timeSince)
			{
				timeSince = 0;
				foreach (GameObject gun in muzzles)
				{
					if (gun != null)
					{
						Rigidbody clone;
						clone = Instantiate(bullet, gun.transform.position, gun.transform.rotation) as Rigidbody;
					}
				}
			}
			if (bearRate < bearTime)
			{
				bearTime = 0;
				if (cannon != null)
				{
					Rigidbody bearClone;
					bearClone = Instantiate(ball, cannon.transform.position, cannon.transform.rotation) as Rigidbody;
				}
			}
		}

	}
	void OnGUI()
	{
		Screen.showCursor = false;
		mouse = Input.mousePosition;
		crossPOS.x = mouse.x-(crossSIZE*0.5f);
		crossPOS.y = Screen.height-mouse.y-(crossSIZE*0.5f);
		GUI.DrawTexture(new Rect(crossPOS.x, crossPOS.y, crossSIZE, crossSIZE), crosshair);
	}
}
