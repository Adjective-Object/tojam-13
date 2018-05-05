using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float walkspeed = 0.5f;
	public float fallspeed = 0.1f;
	public float maxFallSpeed = 0.3f;
	public float friction = 0.2f;
	public float standingGroundedSpeedThreshold = 0.0001f;
	public float jumpInitialVelocity = 0.3f;
	public AbstractController controller;

	private Vector3 mVelocity = new Vector3(0, 0);
	private SpriteAnimator mAnimator;
	private BoxCollider mCollider;
	private Rigidbody mRigidBody;	
	private CharacterController mCharacterController;

	// Use this for initialization
	void Start () {
		mAnimator = GetComponent<SpriteAnimator>();
		mCollider = GetComponent<BoxCollider>();
		mRigidBody = GetComponent<Rigidbody>();
		mCharacterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 iv= controller.GetIntendedVelocity();
		if (iv.magnitude > 1) {
			iv *= 1/ iv.magnitude;
		}
		mVelocity += new Vector3(iv.x, 0, iv.y);
		mVelocity.x *= 1 - friction;
		mVelocity.z *= 1 - friction;

		if (!IsGrounded()) {
			mVelocity.y -= fallspeed;
		}
		else if (controller.ShouldJump()) {
			mVelocity.y = jumpInitialVelocity;
		}
		mVelocity.y = Mathf.Max(-maxFallSpeed, mVelocity.y);

		mCharacterController.Move(mVelocity * Time.deltaTime);

		if (mVelocity.x > 0) {
			mAnimator.SetFlipped(true);
		} else if (mVelocity.x < 0) {
			mAnimator.SetFlipped(false);
		}

		if (IsGrounded()) {
			float groundedSpeed = new Vector2(mVelocity.x, mVelocity.z).magnitude;
			if (Math.Abs(groundedSpeed - 0f) < standingGroundedSpeedThreshold) {
				mAnimator.SetAnimationName("stand");
				mAnimator.SetAnimationSpeed(0);
			} else {
				mAnimator.SetAnimationName("walk");
				mAnimator.SetAnimationSpeed(groundedSpeed);
			}
		} else {
			mAnimator.SetAnimationName("jumpRise");
		}
	}

	// void OnDrawGizmos() {
	//     Gizmos.DrawLine(transform.position, transform.position - Vector3.up * raycastDist);
	// }

	bool IsGrounded() {
		return mCharacterController.isGrounded;
	}
}
