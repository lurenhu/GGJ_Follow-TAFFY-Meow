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
    [Header("��λ����")]
    private BodyType bodyType = BodyType.kHead;
    //[SerializeField]
    //[Header("��������ײϵͳ")]
    //private DamageSystem fatherDamageSystem;
    [SerializeField]
    [Header("�Է�����ϵͳ")]
    private HealthSystem healthSystem;
    [SerializeField]
    [Header("ȭͷ����")]
    private Rigidbody2D fistRb;
    [SerializeField]
    [Header("ȭͷ��ײ��")]
    private Collider2D fistCollider;
    [SerializeField]
    [Header("�޵�ʱ��")]
    private float maxDisDamageableTime = 0.5f;
    [SerializeField]
    [Header("�޵�ʱ���ʱ��")]
    private float disDamageableTime = 0;
    [SerializeField]
    [Header("���������ϵ������˺�ϵͳ")]
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
