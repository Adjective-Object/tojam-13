using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashManager : MonoBehaviour {

	public GameObject muzzleFlash;
	public float muzzleFlashLifetime = 5.0f;
	struct MuzzleFlashInstance {
		public float deathTime;
		public GameObject gameObject;
		public MuzzleFlashInstance(float deathTime, GameObject gameObject) {
			this.deathTime = deathTime;
			this.gameObject = gameObject;
		}
	};
	LinkedList<MuzzleFlashInstance> instances = new LinkedList<MuzzleFlashInstance>();
	
	// Update is called once per frame
	void Update () {
		while (instances.Count > 0) {
			MuzzleFlashInstance instance = instances.First.Value;
			if (instance.deathTime > Time.realtimeSinceStartup) break;
			instances.RemoveFirst();
		}
	}

	public void AddMuzzleFlash() {
		GameObject flash = GameObject.Instantiate(muzzleFlash);
		flash.transform.SetParent(this.transform, false);
		flash.transform.SetParent(null);
		flash.transform.RotateAround(flash.transform.position, Vector3.right, 90);
		instances.AddLast(new MuzzleFlashInstance(
			Time.realtimeSinceStartup + muzzleFlashLifetime,
			flash
		));
	}
}
