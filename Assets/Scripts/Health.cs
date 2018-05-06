using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHurtbox {
    public AbstractController controller;
    public int maxHitpoints = 10;
    public int mHitpoints = 10;
    private bool mIsDead = false;
    private Dictionary<int, Action> changeListeners = new Dictionary<int, Action>();
    int actionListenerCount = 0;
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
        foreach(Action action in changeListeners.Values) {
            action();
        }
    }

    public int RegisterChangeListener(Action a) {
        this.changeListeners.Add(++actionListenerCount, a);
        return actionListenerCount;
    }

    public void DeRegisterChangeListener(int actionListenerID) {
        if (!changeListeners.ContainsKey(actionListenerID)) {
            Debug.LogWarning("tried to deregister already deregistered action listener id=" + actionListenerID);
            return;
        }
        changeListeners.Remove(actionListenerID);
    }

}
