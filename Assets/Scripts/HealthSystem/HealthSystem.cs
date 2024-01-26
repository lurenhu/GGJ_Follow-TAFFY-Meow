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
    [Header("��ʼ���Ѫ��")]
    [SerializeField]
    private float maxHealth;
    [Header("����Ѫ��")]
    [SerializeField]
    private float maxCurrentHealth;
    [Header("��ʼ��Ѫ��,�µĿ�Ѫ��Ϊ��ʼ��Ѫ�����Ա���")]
    [SerializeField]
    private float primeHealthConsume;
    [Header("����,���㹫ʽΪ���˱��ʼӳɳ���Ŀ������ٶ�")]
    [SerializeField]
    private float ratio;
    [Header("����λ���˱��ʼӳ�,����Ϊͷ�����壬��ʼΪ1.5,1")]
    [SerializeField]
    private float[] hurtRatio = new float[] { 1.5f, 1.0f };
    [Header("��С����˺��ٶ�")]
    [SerializeField]
    private float minDamageSpeed = 5.0f;
    [Header("�Է�Ѫ��ϵͳ��Rect")]
    [SerializeField]
    private RectTransform opponentRect;
    [Header("�Է�Ѫ��ϵͳ���������Rect")]
    [SerializeField]
    private RectTransform healthRect;
    [Header("Ѫ�����뷽��")]
    [SerializeField]
    private RectTransform.Edge edge;
    //��ȡѪ��
    public float getCurrentHealth { get { return maxCurrentHealth; } }
    //[Header("����λ��ײ��")]
    //[SerializeField]
    //private Rigidbody2D[] HurtRigi;
    [Space(10)]
    [Header("�˺��¼�")]
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

    //��������
    public void Hurt(HurtType hurtType, Collision2D collision, params object[] elems)
    {
        // ��ȡ��ײ��ͷ�����ײ�ٶȣ����з�����ײ�ٶ��� ��ײ������ٶ� �� ��һ����ײ��ķ��� ��˵ó�
        Vector2 hitPosition = collision.GetContact(0).point;
        float hitSpeed = math.abs(
            Vector2.Dot(
                collision.relativeVelocity,
                collision.GetContact(0).normal.normalized
            )
        );
        // �жϷ�����ײ�ٶ��Ƿ��������˺��ٶ�
        if (hitSpeed < minDamageSpeed) return;
        // ����ײ���Ƿ���HitEffects���,���򴫲β�����Ч
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
        Debug.Log("Ѫ�ۣ�" + maxCurrentHealth + " " + "���ʣ�" + ratio + " " +
            "ȭͷ�ٶȣ�" + hitSpeed + " " + "�˺��ӳɣ�" + hurtRatio[(int)hurtType]);
    }
}
