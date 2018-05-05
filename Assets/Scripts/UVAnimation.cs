using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVAnimation : MonoBehaviour {
	public float playbackSpeed = 10f;
	private SpriteRenderer spr;
	private Sprite [] sprites;
	private float mAnimationTime = 0;
	private float mAnimationSpeed;
	
	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		sprites = Resources.LoadAll<Sprite> ("CloakRedWalk");
	}
	
	void Update(){
		mAnimationTime += Time.deltaTime * mAnimationSpeed * playbackSpeed;
		Debug.Log("speed " + mAnimationSpeed + "time " + mAnimationTime);
		spr.sprite = sprites[(int)mAnimationTime % sprites.Length];
	}

	public void SetAnimationSpeed(float speed) {
		mAnimationSpeed = speed;
	}

	public void Reset() {
		mAnimationTime = 0;
	}
}
