using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public bool hitsMultipleEnemies = false;
    public Collider hitbox;
    public int damage = 1;
    List<GameObject> mCollidedObjects = new List<GameObject>();
    ICanDie attachedDieable;

	// Use this for initialization
	void Start () {
        if (attachedDieable == null) attachedDieable = GetComponent<ICanDie>();
	}

    void OnTriggerEnter(Collider collider)
    {
        IHurtbox hurtbox = collider.gameObject.GetComponent<IHurtbox>();
        if (hurtbox == null) return;
        // Debug.Log("collid with: " + collider + " .. " + hurtbox);

        // Destroy by default
        if (attachedDieable == null) {
            GameObject.Destroy(this.gameObject);
        } else {
            this.attachedDieable.Die();
        }
        hurtbox.OnHit(this);
        // Otherwise, rely on ICanDie to kill it
    }
}
