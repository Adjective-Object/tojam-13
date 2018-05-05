using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour {
   [System.Serializable]
	public struct Animation {
		public string name;
		public int[] frames;
	};
	public Animation[] animations;
	public float playbackSpeed = 100f;
	public string initialAnimation;

	private SpriteRenderer mSpriteRenderer;
	private Sprite [] mSprites;
	private Dictionary<string, Animation> mAnimationDict;
	private float mAnimationTime = 0;
	private float mAnimationSpeed;
	
	private string mCurrentAnimation;
	public string currentAnimation {
		set {
			mCurrentAnimation = value;
		}
	}

	void Start () {
		mAnimationDict = new List<Animation>(animations).ToDictionary(anim => anim.name, anim => anim);
		mSpriteRenderer = GetComponent<SpriteRenderer> ();
		mSprites = Resources.LoadAll<Sprite> (mSpriteRenderer.sprite.texture.name);
		mCurrentAnimation = initialAnimation;
	}
	
	void Update(){
		mAnimationTime += Time.deltaTime * mAnimationSpeed * playbackSpeed;
		Animation currentAnimation = GetCurrentAnimation();
		int currentFrame = (int)mAnimationTime % currentAnimation.frames.Length;
		int currentSprite = currentAnimation.frames[currentFrame];
		mSpriteRenderer.sprite = mSprites[currentSprite];
	}

	public void SetAnimationSpeed(float speed) {
		mAnimationSpeed = speed;
	}

	public void Reset() {
		mAnimationTime = 0;
	}

	public void SetAnimationName(string name) {
		if (name == this.mCurrentAnimation) return;
		mAnimationTime = 0;
		this.mCurrentAnimation = name;
	}

	private Animation GetCurrentAnimation() {
		return this.mAnimationDict[mCurrentAnimation];
	}
}
