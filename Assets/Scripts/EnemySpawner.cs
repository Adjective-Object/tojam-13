using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    List<GameObject> Enemies;
    GameObject Player;

	// Use this for initialization
	void Start () {
        Enemies = new List<GameObject>();
        Player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
        

		foreach(GameObject enemy in Enemies)
        {
        }

        while (Enemies.Count < 1)
        {
            GameObject newEnemy = (GameObject)Instantiate(Resources.Load("Enemy"));

            Vector2 spawn = new Vector2();
            do
            {
                spawn.x = Random.Range(-10.0f, 10.0f);
                spawn.y = Random.Range(-10.0f, 10.0f);
            } while (Vector2.Distance(spawn, Vector2.zero) > 10);

            newEnemy.transform.position = Player.transform.position + new Vector3(spawn.x, 0, spawn.y);
            Enemies.Add(newEnemy);
        }
	}
}
