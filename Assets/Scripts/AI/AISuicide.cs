using UnityEngine;
using System.Collections;

public class AISuicide : AI {
	public GameObject detonator;
	int damageVal;


	Vector3 movement;
	bool hitPlayer;

	// Use this for initialization
	public override void Start () {
		base.Start();
		hp = 100;
		hitPlayer = false;
		damageVal = 20;
	}




	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Player") {
			hitPlayer = true;
		}
		if (hit.gameObject.tag == "Bullet1") {
			subtractHealth(10);
			Destroy (hit.gameObject);
		}
		if (hit.gameObject.tag == "Bullet2") {
			subtractHealth(40);
		}
	}
	
	// Update is called once per frame
	public override void Update () {
		transform.LookAt (player.transform.position);
		movement = new Vector3 (0.0f, 0.0f, 1.0f);
		movement = transform.TransformDirection(movement);
		movement.Normalize();
		movement *= 20f;
		controls.Move (movement*Time.deltaTime);
		if (hitPlayer) {
			playerScript.subtractHealth(damageVal);
			Instantiate(detonator, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
		if (hp <= 0)
		{
			Instantiate(detonator, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
