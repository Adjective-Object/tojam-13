using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Manages a series of hearts
public class HealthIndicator : MonoBehaviour {

	public GameObject indicatorPrefab;
	public Health health;
	public Vector3 indicatorOffset = new Vector3(1, 0, 0);
	public int healthPerIndicator = 4;
	private List<GameObject> mHealthPrefabs = new List<GameObject>();

	// Use this for initialization
	void Start () {
		int numIndicators = (int)Mathf.Ceil(health.maxHitpoints / (float) healthPerIndicator);
		Vector3 startingPosition = -indicatorOffset * ((float)(numIndicators - 1) / 2);
		for (int i=0; i< numIndicators; i++) {
			GameObject indicatorPrefabInstance = Instantiate(indicatorPrefab);
			indicatorPrefabInstance.transform.parent = this.gameObject.transform;
			indicatorPrefabInstance.transform.localPosition = startingPosition + indicatorOffset * i;
			mHealthPrefabs.Add(indicatorPrefabInstance);
		}
	}
	
	// Update is called once per frame
	void Update () {
		int numActiveIndicators = (int)(health.HP / healthPerIndicator);
		for (int i=0; i<mHealthPrefabs.Count; i++) {
			int healthThisIndicator = (i == numActiveIndicators)
				? health.HP - (healthPerIndicator * numActiveIndicators)
				: (i > numActiveIndicators) ? 0 : healthPerIndicator;
			mHealthPrefabs[i].SetActive(healthThisIndicator > 0);
			if (healthThisIndicator > 0) {
				mHealthPrefabs[i].GetComponent<Heart>().SetIndicatorLevel(healthThisIndicator);
			}
		}
	}
}
