using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour {
	// public Dictionary<string, List<int>> frames;
	public float playbackSpeed = 100f;

	private SpriteRenderer mSpriteRenderer;
	private Sprite [] mSprites;
	private float mAnimationTime = 0;
	private float mAnimationSpeed;
	
	void Start () {
		mSpriteRenderer = GetComponent<SpriteRenderer> ();
		mSprites = Resources.LoadAll<Sprite> (mSpriteRenderer.sprite.texture.name);
	}
	
	void Update(){
		mAnimationTime += Time.deltaTime * mAnimationSpeed * playbackSpeed;
		mSpriteRenderer.sprite = mSprites[(int)mAnimationTime % mSprites.Length];
	}

	public void SetAnimationSpeed(float speed) {
		mAnimationSpeed = speed;
	}

	public void Reset() {
		mAnimationTime = 0;
	}
}
