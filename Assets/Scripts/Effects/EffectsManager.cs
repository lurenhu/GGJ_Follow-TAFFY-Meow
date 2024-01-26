using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [Header("粒子组件")]
    public ParticleSystem componentParticle;
    public ParticleSystem sparkParticle;
    public ParticleSystem splashParticle;

    public event Action OnHitEvent = delegate{};

    static public List<HitEffects> hitEffects = new List<HitEffects>();


    private void Start()
    {
        for (int i=0; i<hitEffects.Count; i++)
        {
            hitEffects[i].OnHitEvent += OnHit;
        }
    }

    private void OnHit(Vector2 hitPosition, float hitSpeed, HitEffects.HitEffectsData hitEffectsData)
    {
        OnHitEvent.Invoke();
        if ((hitEffectsData.hitEffectsEnum & HitEffects.HitEffectsEnum.componentParticle) != 0)
        {
            componentParticle.transform.position = hitPosition;
            ParticleSystem.MainModule main = componentParticle.main;
            main.startSpeed = hitSpeed
                * math.max(hitEffectsData.componentStartSpeedFactor, 0.0f);
            int componentCount = (int)(
                hitSpeed
                * math.max(hitEffectsData.componentCountFactor, 0.0f)
            );
            componentParticle.Emit(componentCount);
        }
        if ((hitEffectsData.hitEffectsEnum & HitEffects.HitEffectsEnum.splashParticle) != 0)
        {
            sparkParticle.transform.position = hitPosition;
            sparkParticle.Emit(20);
        }
        if ((hitEffectsData.hitEffectsEnum & HitEffects.HitEffectsEnum.splashParticle) != 0)
        {
            splashParticle.transform.position = hitPosition;
            splashParticle.Emit(1);
        }
    }

    private void PlayHitEffects(Vector3 position, float velocity)
    {
        
    }
}
