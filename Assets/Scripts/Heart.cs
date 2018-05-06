using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Manages the image for a single heart
public class Heart : MonoBehaviour {

	public float displayTimeOnChange = 0.6f;
	public float fadeOutDuration = 0.3f;
	public SpriteRenderer spriteRenderer;
	Sprite[] mSprites;
	private float mLastChangeTime;
	private float mLastOpacity = -1000;


	void Start () {
		if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
		mSprites = Resources.LoadAll<Sprite> (spriteRenderer.sprite.texture.name);
		mLastChangeTime = -(displayTimeOnChange + fadeOutDuration);
	}

	
	// Update is called once per frame
	void Update () {
		float opacity = GetSpriteOpacity();
		if (opacity != mLastOpacity) {
			this.spriteRenderer.color = new Color(1, 1, 1, opacity);
		}
	}

	private float GetSpriteOpacity() {
		float timeSinceUpdate = Time.realtimeSinceStartup - mLastChangeTime;
		if (timeSinceUpdate < displayTimeOnChange) {
			return 1;
		}
		if (timeSinceUpdate < displayTimeOnChange + fadeOutDuration) {
			return Mathf.Clamp(
				1 - ((timeSinceUpdate - displayTimeOnChange) / fadeOutDuration),
				0,
				1
			);
		}
		return 0;
	}
	
	public void SetIndicatorLevel(int health) {
		if (health <= 0 || health > mSprites.Length) {
			Debug.LogWarning(
				"SetIndicatorhealth called with health=" + health + 
				", limit is " + mSprites.Length
			);
			return;
		}
		spriteRenderer.sprite = mSprites[mSprites.Length - health];
		mLastChangeTime = Time.realtimeSinceStartup;
	}
}
