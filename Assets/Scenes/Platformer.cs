using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour {

	public float fallspeed = 1f;
	public float walkspeed = 0.5f;

	public float friction = 0.2f;

	private Vector2 velocity = new Vector2(0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 iv= GetIntendedVelocity();
		Debug.Log(iv);
		velocity += iv * Time.deltaTime;
		velocity *= 1 - friction;
		transform.position += new Vector3(velocity.x, velocity.y, 0);
	}

	Vector2 GetIntendedVelocity() {
		return (Input.GetKey("left") ? new Vector2(-walkspeed, 0) : new Vector2(0, 0)) +
			(Input.GetKey("right") ? new Vector2(walkspeed, 0) : new Vector2(0, 0)) +
			(Input.GetKey("up") ? new Vector2(0, walkspeed) : new Vector2(0, 0)) +
			(Input.GetKey("down") ? new Vector2(0, -walkspeed) : new Vector2(0, 0));
	}
}
