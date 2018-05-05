using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float fallspeed = 1f;
	public float walkspeed = 0.5f;
	public float friction = 0.2f;
	public float standThreshold = 0.0001f;

	AbstractController controller;

	private Vector2 mVelocity = new Vector2(0, 0);
	private SpriteAnimator mAnimator;

	// Use this for initialization
	void Start () {
		mAnimator = GetComponent<SpriteAnimator>();
		controller = GetComponent<AbstractController>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 iv= controller.GetIntendedVelocity();
		mVelocity += iv * Time.deltaTime;
		mVelocity *= 1 - friction;
		transform.position += new Vector3(mVelocity.x, 0, mVelocity.y);

		float speed = mVelocity.magnitude;
		if (Math.Abs(speed - 0f) < standThreshold) {
			mAnimator.SetAnimationName("stand");
			mAnimator.SetAnimationSpeed(0);
		} else {
			mAnimator.SetAnimationName("walk");
			mAnimator.SetAnimationSpeed(speed);
		}
	}
}
