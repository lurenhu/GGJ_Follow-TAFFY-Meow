using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitEffectsManager : MonoBehaviour
{
    // 粒子组件
    public ParticleSystem componentParticle;
    public ParticleSystem sparkParticle;
    public ParticleSystem splashParticle;
    [Tooltip("最小击打速度")]
    public float minHitVelocity = 10.0f;
    [Tooltip("零件初始速度系数")]
    public float componentStartSpeedFactor = 3.0f;
    [Tooltip("零件数量系数")] 
    public float componentCountFactor = 0.5f;
    
    private void OnCollisionEnter2D(Collision2D other)
    {

        Vector2 position = other.GetContact(0).point;
        float velocity = math.abs(
            Vector2.Dot(
                other.relativeVelocity,
                other.GetContact(0).normal.normalized
            )
        );
        if (velocity >= minHitVelocity)
        {
            PlayHitEffects(position, velocity);
        }
    }

    private void PlayHitEffects(Vector3 position, float velocity)
    {
        if (componentParticle)
        {
            componentParticle.transform.position = position;
            componentParticle.startSpeed = velocity * math.max(componentStartSpeedFactor, 0.0f);
            int componentCount = (int)(
                (velocity - minHitVelocity)
                * math.max(componentCountFactor, 0.0f)
            );
            componentParticle.Emit(componentCount);
        }
        if (sparkParticle)
        {
            sparkParticle.transform.position = position;
            sparkParticle.Emit(20);
        }
        if (sparkParticle)
        {
            splashParticle.transform.position = position;
            splashParticle.Emit(1);
        }
    }
}
