using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Manages the image for a single heart
public class Heart : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	Sprite[] mSprites;
	void Start () {
		if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
		mSprites = Resources.LoadAll<Sprite> (spriteRenderer.sprite.texture.name);
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
	}
}
