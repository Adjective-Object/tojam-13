using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public Shoot.Gun gun;

	void OnTriggerEnter (Collider other) {
		Shoot shooter = other.gameObject.GetComponent<Shoot>();
		shooter.gun = gun;
	}
}
