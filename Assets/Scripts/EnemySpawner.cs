using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public int enemyCap = 10;
    public List<GameObject> EnemyPrefabs;
    List<GameObject> Enemies;
    GameObject Player;

    float wave;

	// Use this for initialization
	void Start () {
        Enemies = new List<GameObject>();
        Player = GameObject.Find("player");
        if (EnemyPrefabs == null || EnemyPrefabs.Count == 0) {
            throw new ArgumentException(
                "No prefabs set"
            );
        }
        wave = -100;
	}

    public void Remove(GameObject enemy)
    {
        Enemies.Remove(enemy);
    }

    // Update is called once per frame
    void Update () {

        if (Time.realtimeSinceStartup - wave > 30)
        {
            enemyCap++;
            wave = Time.realtimeSinceStartup;
            for(int i = 0; i < enemyCap; i++)
            {
                GameObject enemyPrefab = EnemyPrefabs[UnityEngine.Random.Range(0, EnemyPrefabs.Count)];
                GameObject newEnemyInstance = (GameObject)Instantiate(enemyPrefab);

                newEnemyInstance.transform.position = new Vector3(-14, 4, -67);
                /*
                Vector2 spawn = new Vector2();
                do
                {
                    spawn.x = UnityEngine.Random.Range(-40.0f, 40.0f);
                    spawn.y = UnityEngine.Random.Range(-40.0f, 40.0f);
                } while (Vector2.Distance(spawn, Vector2.zero) > 20);
                */
                //newEnemyInstance.transform.position = Player.transform.position + new Vector3(spawn.x, 0, spawn.y);
                Enemies.Add(newEnemyInstance);
            }
        }
	}
}
