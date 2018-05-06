using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {


    public GameObject player;
    Vector3 delta;

	// Use this for initialization
	void Start () {
        delta = transform.position - player.transform.position;
        delta.y = 0;
        if (player == null) player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 velocity = Vector3.zero;
        Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) + delta;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.1f);
    }
}
