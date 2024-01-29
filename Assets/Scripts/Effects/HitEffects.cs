using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    [Tooltip("击中特效枚举")]
    public HitEffectsEnum hitEffectsEnum = 0;
    [Tooltip("零件初始速度系数")]
    public float componentStartSpeedFactor = 3.0f;
    [Tooltip("零件数量系数")] 
    public float componentCountFactor = 0.5f;
    [Tooltip("抖动物体")]
    public Transform shakeTransform;
    public float transformShakeFactor = 0.01f;

    public float transformShakeFrameRate = 15.0f;

    // public event Action<Vector2, float, HurtType, HitEffectsData> OnHitEvent = delegate{};

    private Vector3 shakeTransformOriginalPosition;
    private float shakeTransformAngle = 0.0f;
    private float transformShakeIntensity = 0.0f;


    private void Start()
    {
        if (shakeTransform)
        {
            shakeTransformOriginalPosition = shakeTransform.localPosition;
        }
        
    }

    // private void OnEnable()
    // {
    //     EffectsManager.hitEffects.Add(this);
    // }

    // private void OnDisable()
    // {
    //     EffectsManager.hitEffects.Remove(this);
    // }

    public void Play(float hitSpeed)
    {
        ShakeTransform(hitSpeed * transformShakeFactor);
    }

    private void ShakeTransform(float intensity)
    {
        if (!shakeTransform) return;
        if (intensity <= 0.0f) return;
        
        StopCoroutine(ShakeTransformCoroutine(shakeTransform));
        StopCoroutine(ShakeTransformFadeCoroutine(shakeTransform));
        transformShakeIntensity = intensity;
        StartCoroutine(ShakeTransformCoroutine(shakeTransform));
    }

    private IEnumerator ShakeTransformCoroutine(Transform transform)
    {
        StartCoroutine(ShakeTransformFadeCoroutine(transform));
        float timeAccumulation = 1.0f/transformShakeFrameRate;
        while (transformShakeIntensity > 0)
        {
            if (timeAccumulation >= 1.0f/transformShakeFrameRate)
            {
                timeAccumulation = 0.0f;
                shakeTransformAngle += UnityEngine.Random.Range(60.0f, 300.0f);
                shakeTransformAngle %= 360.0f;
                Vector3 transformShakeVector = new Vector2(
                    math.cos(shakeTransformAngle/360.0f*2*math.PI),
                    math.sin(shakeTransformAngle/360.0f*2*math.PI)
                ) * transformShakeIntensity;
                transform.localPosition = shakeTransformOriginalPosition + transformShakeVector;
            }
            else
            {
                timeAccumulation += Time.deltaTime;
            }
            yield return null;
        }
        transform.localPosition = shakeTransformOriginalPosition;
    }

    private IEnumerator ShakeTransformFadeCoroutine(Transform transform)
    {
        transformShakeIntensity = math.max(transformShakeIntensity, 0.0f);
        while (transformShakeIntensity > 0)
        {
            transformShakeIntensity -= Time.deltaTime;
            yield return null;
        }
    }
}
