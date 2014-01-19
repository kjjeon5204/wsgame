using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	protected int hp;
	protected GameObject player;
	protected CharacterController controls;
	protected World world;
	protected IceBerg playerScript;

	/*Subtracts HP*/
	public void subtractHealth (int damage) {
		hp -= damage;
	}


	void OnDestroy() {
		world.object_is_destroyed();
	}



	// Use this for initialization
	public virtual void Start () {
		controls = GetComponent <CharacterController> ();
		player = GameObject.Find("player");
		world = player.GetComponent<World>();
		playerScript = player.GetComponent<IceBerg>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}
}
