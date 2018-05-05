using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour {

	public UVAnimation animator;

	public float fallspeed = 1f;
	public float walkspeed = 0.5f;

	public float friction = 0.2f;

	public float standThreshold = 0.0001f;

	private Vector2 velocity = new Vector2(0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 iv= GetIntendedVelocity();
		velocity += iv * Time.deltaTime;
		velocity *= 1 - friction;
		transform.position += new Vector3(velocity.x, velocity.y, 0);

		float speed = velocity.magnitude;
		if (Math.Abs(speed - 0f) < standThreshold) {
			animator.Reset();
			animator.SetAnimationSpeed(0);
		} else {
			animator.SetAnimationSpeed(speed);
		}
	}

	Vector2 GetIntendedVelocity() {
		return (Input.GetKey("left") ? new Vector2(-walkspeed, 0) : new Vector2(0, 0)) +
			(Input.GetKey("right") ? new Vector2(walkspeed, 0) : new Vector2(0, 0)) +
			(Input.GetKey("up") ? new Vector2(0, walkspeed) : new Vector2(0, 0)) +
			(Input.GetKey("down") ? new Vector2(0, -walkspeed) : new Vector2(0, 0));
	}
}
