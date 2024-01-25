using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyType
{
    kWhole,
    kHead,
    kBody
}
public class DamageSystem : MonoBehaviour
{
    [SerializeField]
    [Header("部位类型")]
    private BodyType bodyType = BodyType.kWhole;
    [SerializeField]
    [Header("父物体碰撞系统")]
    private DamageSystem fatherDamageSystem;
    [SerializeField]
    [Header("对方健康系统")]
    private HealthSystem healthSystem;
    [SerializeField]
    [Header("拳头刚体")]
    private Rigidbody2D fistRb;
    [SerializeField]
    [Header("拳头碰撞体")]
    private Collider2D fistCollider;
    //是否可以打击产生伤害
    private bool isCanHit = true;
    // Start is called before the first frame update
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(1);
        if (fistCollider == collision)
        {
            //Debug.Log(2 + " " + fatherDamageSystem + " " + bodyType + " " + fatherDamageSystem.isCanHit);
            if (fatherDamageSystem != null && bodyType != BodyType.kWhole
    && fatherDamageSystem.isCanHit == true)
            {
                //Debug.Log(3);
                if (bodyType == BodyType.kHead)
                {
                    //Debug.Log(4 + " " + fistRb.velocity.magnitude);
                    healthSystem.Hurt(HurtType.kHead, fistRb.velocity.magnitude);
                }
                else
                {
                    //Debug.Log(5);
                    healthSystem.Hurt(HurtType.kBody, fistRb.velocity.magnitude);
                }
                fatherDamageSystem.isCanHit = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (fistCollider == collision) isCanHit = true;
        //Debug.Log(fistCollider + " " + collision + " " + this.gameObject.name);
    }
}
