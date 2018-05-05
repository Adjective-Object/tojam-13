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

    public override bool ShouldSetPointing()
    {
        return true;
    }

    public override float GetIntendedPointingDegrees()
    {
        float angle = Vector2.Angle(new Vector2(transform.position.x, transform.position.z), new Vector2(Player.transform.position.x, Player.transform.position.z));
        //Debug.Log("Angle: " + angle);
        return angle;
    }

    public override bool ShouldJump()
    {
        return false;
    }
}
