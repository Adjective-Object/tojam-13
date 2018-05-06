using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHurtbox {
    public AbstractController controller;
    public int maxHitpoints = 10;
    public int mHitpoints = 10;
    private bool mIsDead = false;
    public int HP {
        get { return mHitpoints; }
    }
    
    void Start() {
        mHitpoints = maxHitpoints;
        if (controller == null) controller = GetComponent<AbstractController>();
    }

    void Update() {
        if (!mIsDead && this.mHitpoints <= 0) {
            mIsDead = true;
            controller.Die();
        }
    }

    public void OnHit(Attack a) {
        this.mHitpoints -= a.damage;
    }
}
