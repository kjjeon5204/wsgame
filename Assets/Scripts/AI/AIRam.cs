using UnityEngine;
using System.Collections;

public class AIRam : AI {
	Vector3 target;
	float maxRotVal;
	float rotAcceleration;
	float rotDecelerationHit;
	float rotDecelerationMiss;
	float rotVal;
	float ramSpeed;
	/*
	 0 = travling
	 1 = hit
	 2 = miss*/
	int hit;


	enum RammerState {
		PASSIVE,
		ANGRY
	}

	RammerState curState;


	bool random_attack_trigger (int num) {
		int num1, num2;
		num1 = Random.Range(0, num);
		num2 = Random.Range (0,num);

		if (num1 == num2) {
			return true;
		}
		return false;
	}


	// Use this for initialization
	public override void Start () {
		base.Start();
		hp = 300;
		maxRotVal = 2080.0f;
		rotAcceleration = 1000.0f;
		rotDecelerationHit = maxRotVal / 1.0f;
		rotDecelerationMiss = maxRotVal / 3.0f;
		rotVal = 0.0f;

		curState = RammerState.PASSIVE;
	}


	void OnControllerColliderHit(ControllerColliderHit hitTarget) {
		if (hitTarget.gameObject.tag == "Player") {
			/*Input player take damage*/
			playerScript.subtractHealth(20);
			ramSpeed = -ramSpeed;
			hit = 1;
		}
		if (hitTarget.gameObject.tag == "Bullet1") {
			Debug.Log("!");
			subtractHealth (10);
			Destroy(hitTarget.gameObject);
		}
		if (hitTarget.gameObject.tag == "Bullet2") {
			subtractHealth (40);
		}
	}
	
	// Update is called once per frame
	public override void Update () {
		if (hp <= 0) {
			Destroy(this.gameObject);
		}
		/*Event checker*/
		if (curState == RammerState.PASSIVE) {

			if (random_attack_trigger (250)) {
				curState = RammerState.ANGRY;
				hit = 0;
				rotVal = 0.0f;
				target = player.transform.position;
				ramSpeed = 50.0f;
			}

		}
		else if (curState == RammerState.ANGRY) {
			if (hit == 0) {
				if ((transform.position - target).magnitude < 1f) {
					hit = 2;
				}
			}
			else if (hit ==1 || hit ==2) {
				if (rotVal <= 0.0f || ramSpeed == 0.0f) {
					curState = RammerState.PASSIVE;
					transform.Rotate (0.0f, 0.0f, -transform.rotation.eulerAngles.z);
				}
			}
		}


		/*Even handler*/
		if (curState == RammerState.PASSIVE) {
			transform.rotation = Quaternion.identity;
			transform.LookAt (player.transform.position);
			Vector3 transformObj = new Vector3 (1.0f, 0.0f, 0.0f);
			transformObj.Normalize();
			Vector3 dir = transformObj;
			transformObj *= 5.0f;
		
			transformObj = transform.TransformDirection(transformObj);
			controls.Move(transformObj * Time.deltaTime);

			transform.Rotate (-90.0f * Vector3.down);

		}
		else if (curState == RammerState.ANGRY) {
			if (hit == 0 && rotVal < maxRotVal) {
				transform.LookAt (target);
				rotVal += (rotAcceleration * Time.deltaTime);
				transform.Rotate(Vector3.forward, rotVal/* * (Time.deltaTime)*/);
			}
			else if (hit == 0 && rotVal >= maxRotVal) {
				Vector3 moveObj = new Vector3 (0.0f, 0.0f, ramSpeed);
				moveObj = transform.TransformDirection(moveObj);
				controls.Move (moveObj * Time.deltaTime);
				transform.Translate (0.0f, 0.0f, ramSpeed * Time.deltaTime);
				transform.Rotate(Vector3.forward, rotVal/* * (Time.deltaTime)*/);
			}
			else if (hit == 1) {
				Debug.Log ("hit 1");
				Vector3 moveObj = new Vector3 (0.0f, 0.0f, ramSpeed);
				//moveObj = transform.TransformDirection(moveObj);
				transform.Translate (moveObj * Time.deltaTime);
				ramSpeed += 50.0f * Time.deltaTime;
				rotVal -= rotDecelerationHit * Time.deltaTime;
			}
			else if (hit == 2) {
				Debug.Log ("hit 2");
				Vector3 moveObj = new Vector3 (0.0f, 0.0f, ramSpeed);
				moveObj = transform.TransformDirection(moveObj);
				controls.Move (moveObj * Time.deltaTime);
				if (ramSpeed > 0.0f) {
					ramSpeed -= 40.0f * Time.deltaTime;
				}
				rotVal -= rotDecelerationMiss * Time.deltaTime;
			}
			if (transform.position.x >= 137 || transform.position.x <= -137) {
				curState = RammerState.ANGRY;
				hit = 0;
				rotVal = 0.0f;
				target = player.transform.position;
				ramSpeed = 50.0f;
				if (transform.position.x >= 137)
					transform.position = new Vector3(136, transform.position.y, transform.position.z);
				else
					transform.position = new Vector3(-136, transform.position.y, transform.position.z);
			}
			if (transform.position.z >= 57 || transform.position.z <= -57) {
				curState = RammerState.ANGRY;
				hit = 0;
				rotVal = 0.0f;
				target = player.transform.position;
				ramSpeed = 50.0f;
				if (transform.position.z >= 57)
					transform.position = new Vector3(transform.position.x, transform.position.y, 56);
				else
					transform.position = new Vector3(transform.position.x, transform.position.y, -56);
			}
		}
	}
}
