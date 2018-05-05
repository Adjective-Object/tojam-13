using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfter : MonoBehaviour {
	public float duration;
    float mStartTime = 0;

	
	// Update is called once per frame
	void Update () {
        mStartTime = (mStartTime == 0) ? Time.realtimeSinceStartup : mStartTime;
        if (Time.realtimeSinceStartup >= mStartTime + duration) {
            Destroy(this.gameObject);
        }
	}

}
