using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public enum HurtType
{
    kHead,
    kBody
};
public class HealthSystem : MonoBehaviour
{
    [Header("初始最大血槽")]
    [SerializeField]
    private float maxHealth;
    [Header("现在血槽")]
    [SerializeField]
    private float maxCurrentHealth;
    [Header("初始扣血量,新的扣血量为初始扣血量乘以倍率")]
    [SerializeField]
    private float primeHealthConsume;
    [Header("倍率,计算公式为受伤倍率加成乘以目标刚体速度")]
    [SerializeField]
    private float ratio;
    [Header("各部位受伤倍率加成,依次为头，身体，初始为1.5,1")]
    [SerializeField]
    private float[] hurtRatio = new float[] { 1.5f, 1.0f };
    [Header("最小造成伤害速度")]
    [SerializeField]
    private float minDamageSpeed = 5.0f;
    [Header("对方血槽系统的Rect")]
    [SerializeField]
    private RectTransform opponentRect;
    [Header("对方血槽系统中子物体的Rect")]
    [SerializeField]
    private RectTransform healthRect;
    [Header("血条对齐方向")]
    [SerializeField]
    private RectTransform.Edge edge;
    //获取血槽
    public float getCurrentHealth { get { return maxCurrentHealth; } }
    //[Header("各部位碰撞体")]
    //[SerializeField]
    //private Rigidbody2D[] HurtRigi;
    [Space(10)]
    [Header("伤害事件")]
    public UnityEvent onHurt0;
    public UnityEvent<object> onHurt1;
    public UnityEvent<object, object> onHurt2;
    public UnityEvent<object, object, object> onHurt3;

    void Start()
    {
        maxCurrentHealth = maxHealth;
        if (primeHealthConsume == 0) primeHealthConsume = 1.0f;
        if (onHurt0 == null) onHurt0 = new UnityEvent();
        if (onHurt1 == null) onHurt1 = new UnityEvent<object>();
        if (onHurt2 == null) onHurt2 = new UnityEvent<object, object>();
        if (onHurt3 == null) onHurt3 = new UnityEvent<object, object, object>();
    }

    //攻击函数
    public void Hurt(HurtType hurtType, Collision2D collision, params object[] elems)
    {
        // 获取碰撞点和法向碰撞速度，其中法向碰撞速度由 碰撞体相对速度 与 第一个碰撞点的法向 点乘得出
        Vector2 hitPosition = collision.GetContact(0).point;
        float hitSpeed = math.abs(
            Vector2.Dot(
                collision.relativeVelocity,
                collision.GetContact(0).normal.normalized
            )
        );
        // 判断法向碰撞速度是否大于最低伤害速度
        if (hitSpeed < minDamageSpeed) return;
        // 被碰撞体是否有HitEffects组件,有则传参播放特效
        HitEffects hitEffects = collision.otherCollider.gameObject.GetComponent<HitEffects>();
        if (hitEffects) {hitEffects.Play(hitPosition, hitSpeed - minDamageSpeed);}

        switch (elems.Length)
        {
            case 0:
                onHurt0.Invoke();
                break;
            case 1:
                onHurt1.Invoke(elems[0]);
                break;
            case 2:
                onHurt2.Invoke(elems[0], elems[1]);
                break;
            case 3:
                onHurt3.Invoke(elems[0], elems[1], elems[2]);
                break;
            default:
                break;
        }

        ratio = hurtRatio[(int)hurtType] * hitSpeed;
        maxCurrentHealth -= primeHealthConsume * ratio;
        if (maxCurrentHealth < 0)
        {
            maxCurrentHealth = 0;
            healthRect.SetInsetAndSizeFromParentEdge(edge, 0, opponentRect.rect.width);
        }
        else
        {
            healthRect.SetInsetAndSizeFromParentEdge(edge, 0, opponentRect.rect.width *
                (1 - maxCurrentHealth / maxHealth));
        }
        Debug.Log("血槽：" + maxCurrentHealth + " " + "倍率：" + ratio + " " +
            "拳头速度：" + hitSpeed + " " + "伤害加成：" + hurtRatio[(int)hurtType]);
    }
}
