using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum HurtType
{
    kHead,
    kBody
};
public class HealthSystem : MonoBehaviour
{
    [Header("���Ѫ��")]
    [SerializeField]
    private float maxHealth;
    [Header("��ʼ��Ѫ��,�µĿ�Ѫ��Ϊ��ʼ��Ѫ�����Ա���")]
    [SerializeField]
    private float primeHealthConsume;
    [Header("����,���㹫ʽΪ���˱��ʼӳɳ���Ŀ������ٶ�")]
    [SerializeField]
    private float ratio;
    [Header("����λ���˱��ʼӳ�,����Ϊͷ�����壬��ʼΪ1.5,1")]
    [SerializeField]
    private float[] hurtRatio = new float[] { 1.5f, 1.0f };
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
        if (maxHealth == 0) maxHealth = 5.0f;
        if (primeHealthConsume == 0) primeHealthConsume = 1.0f;
        if (onHurt0 == null) onHurt0 = new UnityEvent();
        if (onHurt1 == null) onHurt1 = new UnityEvent<object>();
        if (onHurt2 == null) onHurt2 = new UnityEvent<object, object>();
        if (onHurt3 == null) onHurt3 = new UnityEvent<object, object, object>();
    }

    //��������
    public void Hurt(HurtType hurtType, float hitSpeed, params object[] elems)
    {
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
        maxHealth -= primeHealthConsume * ratio;
        if (maxHealth < 0) maxHealth = 0;
        Debug.Log("Ѫ�ۣ�" + maxHealth + " " + "���ʣ�" + ratio + " " +
            "ȭͷ�ٶȣ�" + hitSpeed + " " + "�˺��ӳɣ�" + hurtRatio[(int)hurtType]);
    }
}
