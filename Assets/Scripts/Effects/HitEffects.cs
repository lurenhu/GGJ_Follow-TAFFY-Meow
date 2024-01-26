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

    public event Action<Collision2D, HitEffectsData> OnHitEvent = delegate{};


    private void OnEnable()
    {
        EffectsManager.hitEffects.Add(this);
    }

    private void OnDisable()
    {
        EffectsManager.hitEffects.Remove(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnHitEvent.Invoke(other, hitEffectsData);
    }

    [Serializable]
    public class HitEffectsData
    {
        public HitEffectsEnum hitEffectsEnum = 0;
        [Tooltip("最小击打速度")]
        public float minHitVelocity = 10.0f;
        [Tooltip("零件初始速度系数")]
        public float componentStartSpeedFactor = 3.0f;
        [Tooltip("零件数量系数")] 
        public float componentCountFactor = 0.5f;
    }
}
