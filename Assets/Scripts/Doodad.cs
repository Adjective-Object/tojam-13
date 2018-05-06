using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodad : MonoBehaviour {

	[System.Serializable]
	public struct DoodadOption {
		public int frame;
		public Vector3 offset;
	};
	public SpriteRenderer spriteRenderer;
	public DoodadOption[] doodadOptions;

	private Sprite[] mSprites;

	// Use this for initialization
	void Start () {
		if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
		mSprites = Resources.LoadAll<Sprite> (spriteRenderer.sprite.texture.name);
		int index = Random.Range(0, this.doodadOptions.Length);
		DoodadOption option = this.doodadOptions[index];
		spriteRenderer.sprite = mSprites[option.frame];
		this.transform.position += option.offset;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
