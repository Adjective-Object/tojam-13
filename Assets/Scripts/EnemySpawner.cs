using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public int enemyCap = 10;
    public List<GameObject> EnemyPrefabs;
    List<GameObject> Enemies;
    GameObject Player;

	// Use this for initialization
	void Start () {
        Enemies = new List<GameObject>();
        Player = GameObject.Find("player");
        if (EnemyPrefabs == null || EnemyPrefabs.Count == 0) {
            throw new ArgumentException(
                "No prefabs set"
            );
        }
	}
	
	// Update is called once per frame
	void Update () {

		foreach(GameObject enemy in Enemies)
        {
        }

        while (Enemies.Count < enemyCap)
        {
            GameObject enemyPrefab = EnemyPrefabs[UnityEngine.Random.Range(0, EnemyPrefabs.Count)];
            GameObject newEnemyInstance = (GameObject)Instantiate(enemyPrefab);

            Vector2 spawn = new Vector2();
            do
            {
                spawn.x = UnityEngine.Random.Range(-10.0f, 10.0f);
                spawn.y = UnityEngine.Random.Range(-10.0f, 10.0f);
            } while (Vector2.Distance(spawn, Vector2.zero) > 10);

            newEnemyInstance.transform.position = Player.transform.position + new Vector3(spawn.x, 0, spawn.y);
            Enemies.Add(newEnemyInstance);
        }
	}
}
