using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Utils : Object
{
    // 求碰撞法向速度
    static public float GetCollisionNormalVelocity(Vector2 velocity, Vector2 normal)
    {
        float normalVelocity = Vector2.Dot(
            velocity,
            normal.normalized
        );
        return normalVelocity;
    }

    static public float GetCollisionNormalVelocity(Vector3 velocity, Vector3 normal)
    {
        float normalVelocity = Vector3.Dot(
            velocity,
            normal.normalized
        );
        return normalVelocity;
    }
}
