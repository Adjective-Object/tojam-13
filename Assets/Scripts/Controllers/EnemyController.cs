using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AbstractController
{
    GameObject Player;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("player");
    }

    public override Vector2 GetIntendedVelocity()
    {
        Vector2 playerPos = new Vector2(Player.transform.position.x, Player.transform.position.z);
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.z);
        if(Vector2.Distance(playerPos, enemyPos) > 5)
        {
            Vector2 velocity = playerPos - enemyPos;
            velocity.Normalize();
            return velocity;
        }
        return Vector2.zero;
    }

    protected override bool ShouldSetPointing()
    {
        return true;
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
        return false;
    }

    public override bool ShouldShoot() {
        return true;
    }
    
    public override bool ShouldReload() {
        return false;
    }
}
