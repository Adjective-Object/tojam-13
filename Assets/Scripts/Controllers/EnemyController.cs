﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AbstractController
{
    GameObject Player;
    public Shoot shoot;
    public float fireRate = 1f;
    bool shouldShoot;
    float lastJump = 0;
    float lastTargetUpdate = 0;
    Vector2 target;
    float jumpingTime;

    bool random;
    float randomTime;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("player");
        shouldShoot = false;
        if (shoot == null) shoot = GetComponent<Shoot>();

        jumpingTime = Random.Range(8, 12);
    }

    public override Vector2 GetIntendedVelocity()
    {


 
        if (random && Time.realtimeSinceStartup - lastTargetUpdate > 10)
        {
            random = false;
        }

        if (random == false)
        {
            if (Time.realtimeSinceStartup - lastTargetUpdate > 2.5 ||
                Vector2.Distance(target, new Vector2(transform.position.x, transform.position.z)) < 3 ||
                Vector2.Distance(target, new Vector2(Player.transform.position.x, Player.transform.position.z)) > 10)
            {
                lastTargetUpdate = Time.realtimeSinceStartup;

                random = Random.value > 0.8f && Time.realtimeSinceStartup - randomTime > 20;
                if (random)
                {
                    randomTime = Time.realtimeSinceStartup;
                    target.x = UnityEngine.Random.Range(-50.0f, 50.0f);
                    target.y = UnityEngine.Random.Range(-50.0f, 50.0f);
                }
                else
                {

                    Vector2 target = new Vector2();
                    Vector2 player = new Vector2(Player.transform.position.x, Player.transform.position.z);
                    do
                    {
                        target.x = UnityEngine.Random.Range(-10.0f, 10.0f);
                        target.y = UnityEngine.Random.Range(-10.0f, 10.0f);
                    } while (Vector2.Distance(target, Vector2.zero) < 5 || Vector2.Distance(target, Vector2.zero) > 10);
                    this.target = player + target;
                }
            }
        }
        

        Vector2 playerPos = target;
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.z);
        if(Vector2.Distance(playerPos, enemyPos) > 5)
        {
            Vector2 velocity = playerPos - enemyPos;
            velocity.Normalize();
            return velocity;
        }
        shouldShoot = (Vector2.Distance(playerPos, enemyPos) < 20) && 
            Random.Range(0, 1.0f) * (1 / Mathf.Max(Time.deltaTime, 0.0001f)) < fireRate;

        return Vector2.zero;
    }

    protected override bool ShouldSetPointing()
    {
        return true;
    }

    public override void Die()
    {
        base.Die();
        EnemySpawner spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        spawner.Remove(this.gameObject);
    }

    protected override float GetIntendedPointingDegrees()
    {
        Vector2 myPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPosition = new Vector2(Player.transform.position.x, Player.transform.position.z);
        Vector2 offset = playerPosition - myPosition;

 		return Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
    }

    public override bool ShouldJump()
    {
        if (Time.realtimeSinceStartup - lastJump > jumpingTime || 
            (Time.realtimeSinceStartup - lastJump > jumpingTime/3 && Player.transform.position.y - transform.position.y > 2))
        {
            lastJump = Time.realtimeSinceStartup;
            return true;
        }
        return false;
    }

    public override bool ShouldShoot() {
        return shouldShoot;
    }
    
    public override bool ShouldReload() {
        return shoot.gun.currentAmmo == 0;
    }
}
