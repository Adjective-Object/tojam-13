using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {
    public AbstractController controller;
    public int maxHitpoints = 10;
    private int mHitpoints = 10;
    public int HP {
        get { return mHitpoints; }
    }
    
    void Start() {
        mHitpoints = maxHitpoints;
        if (controller == null) controller = GetComponent<AbstractController>();
    }

    void Update() {
        if (this.mHitpoints <= 0) {
            controller.Die();
        }            
    }
}
