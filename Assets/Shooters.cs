using UnityEngine;
using System.Collections;

public class Shooters : MonoBehaviour {
	public float speed = 3.75f;
	// Use this for initialization
	void Start () {
		speed = 3.75f;
	}
	
	// Update is called once per frame
	void Update () {
		Plane playerPlane = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float hitdist = 0.0f;
		// If the ray is parallel to the plane, Raycast will return false.
		if (playerPlane.Raycast (ray, out hitdist)) 
		{
			Vector3 targetPoint = ray.GetPoint(hitdist);
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.time);
		}
	}
}
