using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour {

    public int numSceneObjects = 100;
    public int numBuildings = 4;

    public List<GameObject> SceneObjects;
    public List<GameObject> Buildings;

    // Use this for initialization
    void Start () {
		for(int i = 0; i < numSceneObjects; i++)
        {
            GameObject item = SceneObjects[Random.Range(0, SceneObjects.Count)];
            GameObject instance = (GameObject)Instantiate(item);
            instance.transform.position = new Vector3(Random.Range(-50, 50), item.transform.position.y, Random.Range(-50, 50));
        }

        for (int i = 0; i < numBuildings; i++)
        {
            GameObject item = Buildings[Random.Range(0, Buildings.Count)];
            GameObject instance = (GameObject)Instantiate(item);
            instance.transform.position = new Vector3(Random.Range(-50, 50), item.transform.position.y, Random.Range(-50, 50));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
