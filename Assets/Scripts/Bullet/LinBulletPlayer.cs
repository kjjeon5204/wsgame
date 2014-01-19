

using UnityEngine;
using System.Collections;

public class LinBulletPlayer : MonoBehaviour {
	float bulletTime;
	bool hitEnemy;
	public GameObject detonator;
	public int dmg = 10;

	AI enemy;


	void OnCollionEnter (Collision hit) {
		Debug.Log("!");
		if (hit.gameObject.tag == "AI") {
			hitEnemy = true;
			enemy = hit.gameObject.GetComponent<AI>();
		}
	}

	void on_hit() {
		enemy.subtractHealth(dmg);
		Destroy (gameObject);
	}




	// Use this for initialization
	void Start () {
		hitEnemy = false;
		bulletTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward*Time.deltaTime*60);
		if (Time.time >= bulletTime + 2.0f) {
			Destroy (gameObject);
		}
		if (hitEnemy == true) {
			Instantiate (gameObject, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}


