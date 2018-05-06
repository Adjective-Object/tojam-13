using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreIndicator : MonoBehaviour {

	public Text text;
	private float mStartTime;

	// Use this for initialization
	void Start () {
		if (text == null) text = GetComponent<Text>();
		mStartTime = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		float timeSurvived = (Time.realtimeSinceStartup - mStartTime);
		text.text = String.Format("Seconds Alive: {0:0.##}", timeSurvived);
	}
}
