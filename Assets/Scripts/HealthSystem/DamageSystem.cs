using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyType
{
    kHead,
    kBody
}
public class DamageSystem : MonoBehaviour
{
    [SerializeField]
    [Header("部位类型")]
    private BodyType bodyType = BodyType.kHead;
    //[SerializeField]
    //[Header("父物体碰撞系统")]
    //private DamageSystem fatherDamageSystem;
    [SerializeField]
    [Header("对方健康系统")]
    private HealthSystem healthSystem;
    [SerializeField]
    [Header("拳头刚体")]
    private Rigidbody2D fistRb;
    [SerializeField]
    [Header("拳头碰撞体")]
    private Collider2D fistCollider;
    [SerializeField]
    [Header("无敌时间")]
    private float maxDisDamageableTime = 0.5f;
    [SerializeField]
    [Header("无敌时间计时器")]
    private float disDamageableTime = 0;
    [SerializeField]
    [Header("机器人身上的所有伤害系统")]
    private DamageSystem[] damageSystems;
    public BodyType getBodyType { get { return bodyType; } }
    public float getDisDamageableTime { get { return disDamageableTime; } }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == fistCollider)
        {
            foreach (var item in damageSystems)
            {
                if (item.getDisDamageableTime > 0.01f)
                {
                    return;
                }
            }
            switch (bodyType)
            {
                case BodyType.kHead:
                    healthSystem.Hurt(HurtType.kHead, collision);
                    break;
                case BodyType.kBody:
                    healthSystem.Hurt(HurtType.kBody, collision);
                    break;
                default:
                    break;
            }
            disDamageableTime = maxDisDamageableTime;
        }
    }
    private void Update()
    {
        if (disDamageableTime > 0f)
        {
            disDamageableTime -= Time.deltaTime;
        }
        else
        {
            disDamageableTime = 0;
        }
    }
}
