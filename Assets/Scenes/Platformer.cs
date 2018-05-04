using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour {

	public float fallspeed = 1f;
	public float dashspeed = 1f;
	public float walkspeedFast = 3f;
	public float walkspeedSlow = 1f;

	public float friction = 0.2f;

	private Vector2 velocity = new Vector2(0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 iv= GetIntendedVelocity();
		Debug.Log(iv);
		velocity += iv;
		velocity *= 1 - friction;
		transform.position += new Vector3(velocity.x, velocity.y, 0);
	}

	Vector2 GetIntendedVelocity() {
		return (Input.GetKeyDown("left") ? new Vector2(-dashspeed, 0) : new Vector2(0, 0)) +
			(Input.GetKeyDown("right") ? new Vector2(dashspeed, 0) : new Vector2(0, 0)) +
			(Input.GetKeyDown("up") ? new Vector2(0, dashspeed) : new Vector2(0, 0)) +
			(Input.GetKeyDown("down") ? new Vector2(0, -dashspeed) : new Vector2(0, 0));
	}
}
