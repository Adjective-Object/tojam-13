using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour {

	public float fallspeed = 1f;
	public float walkspeed = 0.5f;
	public float friction = 0.2f;
	public float standThreshold = 0.0001f;

	private Vector2 mVelocity = new Vector2(0, 0);
	private SpriteAnimation mAnimator;

	// Use this for initialization
	void Start () {
		mAnimator = GetComponent<SpriteAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 iv= GetIntendedVelocity();
		mVelocity += iv * Time.deltaTime;
		mVelocity *= 1 - friction;
		transform.position += new Vector3(mVelocity.x, 0, mVelocity.y);

		float speed = mVelocity.magnitude;
		if (Math.Abs(speed - 0f) < standThreshold) {
			mAnimator.Reset();
			mAnimator.SetAnimationSpeed(0);
		} else {
			mAnimator.SetAnimationSpeed(speed);
		}
	}

	Vector2 GetIntendedVelocity() {
		return (Input.GetKey("left") ? new Vector2(-walkspeed, 0) : new Vector2(0, 0)) +
			(Input.GetKey("right") ? new Vector2(walkspeed, 0) : new Vector2(0, 0)) +
			(Input.GetKey("up") ? new Vector2(0, walkspeed) : new Vector2(0, 0)) +
			(Input.GetKey("down") ? new Vector2(0, -walkspeed) : new Vector2(0, 0));
	}
}
