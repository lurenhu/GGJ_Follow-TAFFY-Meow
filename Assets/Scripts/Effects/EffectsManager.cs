using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [Header("粒子组件")]
    public ParticleSystem componentParticle;
    public ParticleSystem sparkParticle;
    public ParticleSystem splashParticle;
    public SpriteRenderer splashSprite;
    [Space(10)]
    public float timeScaleDuration = 0.5f;
    [Range(0.0f, 1.0f)]
    public float timeScaleExitTime = 0.3f;
    public float timeScaleMinValue = 0.1f;

    public event Action<bool> OnHitEvent = delegate{};

    static public List<HitEffects> hitEffects = new List<HitEffects>();

    private float xVariable;


    private void Start()
    {
        for (int i=0; i<hitEffects.Count; i++)
        {
            hitEffects[i].OnHitEvent += OnHit;
        }
        xVariable = math.max(timeScaleDuration, 0.0f);
    }

    private void OnHit(Vector2 hitPosition, float hitSpeed, HurtType hurtType, HitEffects.HitEffectsData hitEffectsData)
    {
        if ((hitEffectsData.hitEffectsEnum & HitEffects.HitEffectsEnum.componentParticle) != 0)
        {
            if (!componentParticle) return;
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
            if (!sparkParticle) return;
            sparkParticle.transform.position = hitPosition;
            sparkParticle.Emit(20);
        }
        if ((hitEffectsData.hitEffectsEnum & HitEffects.HitEffectsEnum.splashParticle) != 0)
        {
            if (!splashParticle) return;
            splashParticle.transform.position = hitPosition;
            splashParticle.Emit(1);
        }

        if (hurtType == HurtType.kHead)
        {
            OnHitEvent.Invoke(true);
            xVariable = 0.0f;
            xVariable = math.min(xVariable + Time.unscaledDeltaTime, timeScaleDuration);
            if (splashSprite)
            {
                splashSprite.transform.position = hitPosition;
            }
        }
        else
        {
            OnHitEvent.Invoke(false);
        }
        
    }

    private void PlayHitEffects(Vector3 position, float velocity)
    {
        
    }

    private void Update()
    {
        timeScaleDuration = math.max(timeScaleDuration, 0.0f);
        Time.timeScale = GetTimeScale();
        xVariable = math.min(xVariable + Time.unscaledDeltaTime, timeScaleDuration);
    }

    private float GetTimeScale()
    {
        if (xVariable < timeScaleExitTime * timeScaleDuration)
        {
            if (splashSprite) splashSprite.enabled = true;
            return timeScaleMinValue;
        }
        else
        {
            if (splashSprite) splashSprite.enabled = false;
            return timeScaleMinValue + 
                (xVariable - timeScaleExitTime * timeScaleDuration)
                 / (timeScaleDuration - timeScaleExitTime * timeScaleDuration)
                 * (1.0f - timeScaleMinValue);
        }
    }
}
