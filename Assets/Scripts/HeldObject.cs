using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldObject : MonoBehaviour {
	public float offset;
	public AbstractController controller;
	private SpriteRenderer mSpriteRenderer;

	// Use this for initialization
	void Start () {
		mSpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		SetPointingDegrees(controller.GetPointingDegrees());
	}

	private void SetPointingDegrees(float inputDegrees) {
		float degrees = (inputDegrees + 360f) % 360f;
		this.transform.localRotation = Quaternion.AngleAxis(degrees, Vector3.forward);
		this.transform.localPosition = this.transform.localRotation * new Vector3(offset, 0, 0);
		bool shouldFlip = (degrees > 90 && degrees < 270);
		mSpriteRenderer.flipY = shouldFlip;
	}

}
