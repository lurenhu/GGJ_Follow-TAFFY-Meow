using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitEffects : MonoBehaviour
{
    [System.Flags]
    public enum HitEffectsEnum
    {
        componentParticle = 1 << 0,
        sparkParticle = 1 << 1,
        splashParticle = 1 << 2
    }

    public HitEffectsData hitEffectsData = new HitEffectsData();

    public event Action<Vector2, float, HurtType, HitEffectsData> OnHitEvent = delegate{};


    private void OnEnable()
    {
        EffectsManager.hitEffects.Add(this);
    }

    private void OnDisable()
    {
        EffectsManager.hitEffects.Remove(this);
    }

    public void Play(Vector2 hitPosition, float hitSpeed, HurtType hurtType)
    {
        OnHitEvent.Invoke(hitPosition, hitSpeed, hurtType, hitEffectsData);
    }

    [Serializable]
    public class HitEffectsData
    {
        [Tooltip("击中特效枚举")]
        public HitEffectsEnum hitEffectsEnum = 0;
        [Tooltip("零件初始速度系数")]
        public float componentStartSpeedFactor = 3.0f;
        [Tooltip("零件数量系数")] 
        public float componentCountFactor = 0.5f;
    }
}
