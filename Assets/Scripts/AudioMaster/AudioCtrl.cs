using ClockStone;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum VolumeType
{
    kMusic,
    kAmbience,
    kUISound,
    kGameSound
}
public enum GameSoundType
{
    kDeed,
    kHit,
    kAttack,
    kMove
}
public class AudioCtrl : MonoBehaviour
{
    public static AudioCtrl GetInstance { get; private set; }
    [SerializeField]
    [Header("不同类型的音量，从左到右依次为音乐，氛围，UI音效,游戏音效")]
    [Range(0f, 1f)]
    private float[] audioVolume = new float[] { 0.5f, 0.45f, 0.3f, 0.2f };
    [SerializeField]
    [Header("不同类型的游戏音效权重，从左到右依次为死亡爆炸，重击，受击，移动")]
    [Range(0f, 1f)]
    private float[] gameSoundVolume = new float[] { 1f, 0.8f, 2.0f, 0.4f };
    [SerializeField]
    [Header("不同类型的音乐控制器，从左到右依次为音乐，氛围，UI音效，游戏音效")]
    private AudioSource[] audioSource;
    [SerializeField]
    [Header("随机音高最小值")]
    [Range(-3.0f, 3.0f)]
    private float randomPitchMin;
    [SerializeField]
    [Header("随机音高最大值")]
    [Range(-3.0f, 3.0f)]
    private float randomPitchMax;
    [SerializeField]
    [Header("受击音效列表")]
    private AudioClip[] attackClip;
    [SerializeField]
    [Header("重击音效列表")]
    private AudioClip[] hitClip;
    private EffectsManager effectsManager;

    private void Awake()
    {
        if (GetInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            GetInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        for (int i = 0; i < audioVolume.Length; i++)
        {
            ChangeAudioVolume((VolumeType)i, audioVolume[i]);
        }
        effectsManager = GameObject.Find("EffectsManager").GetComponent<EffectsManager>();
        if (effectsManager != null)
        {
            effectsManager.OnHitEvent += AttackSoundFunc;
        }
    }

    public void ChangeAudioVolume(VolumeType volumeType, float volume)
    {
        this.audioVolume[(int)volumeType] = volume;
        audioSource[(int)volumeType].volume = this.audioVolume[(int)volumeType];
    }

    public void StopOrStartMusic(VolumeType volumeType, bool isStop, AudioClip audioClip = null)
    {
        if (isStop) audioSource[(int)volumeType].Stop();
        else
        {
            if (audioClip == null) Debug.LogError("Audio Clip Is NULL");
            audioSource[(int)volumeType].clip = audioClip;
            audioSource[(int)volumeType].Play();
        }
    }

    public void PauseOrUnPauseMusic(VolumeType volumeType, bool isPause)
    {
        if (isPause) audioSource[(int)volumeType].Pause();
        else audioSource[(int)volumeType].UnPause();
    }

    public void PlaySound(VolumeType volumeType, AudioClip audioClip, GameSoundType gameSoundType = GameSoundType.kDeed)
    {
        if (volumeType != VolumeType.kGameSound) audioSource[(int)volumeType].PlayOneShot(audioClip);
        else
        {
            audioSource[(int)volumeType].pitch = Random.Range(Mathf.Min(randomPitchMin, randomPitchMax),
    Mathf.Max(randomPitchMin, randomPitchMax));
            float lastVolume = audioSource[(int)volumeType].volume;
            audioSource[(int)volumeType].volume = lastVolume * gameSoundVolume[(int)gameSoundType];
            Debug.Log(gameSoundVolume[(int)gameSoundType]);
            audioSource[(int)volumeType].PlayOneShot(audioClip);
            audioSource[(int)volumeType].volume = lastVolume;
        }
    }

    public void UISoundFunc(AudioClip audioClip)
    {
        PlaySound(VolumeType.kUISound, audioClip);
    }

    public void AttackSoundFunc(bool isHardHit)
    {
        if (isHardHit)
        {
            Debug.Log("播放重击");
            PlaySound(VolumeType.kGameSound, hitClip[Random.Range(0, hitClip.Length)], GameSoundType.kHit);
        }
        else
        {
            Debug.Log("播放收击");
            PlaySound(VolumeType.kGameSound, attackClip[Random.Range(0, attackClip.Length)], GameSoundType.kAttack);
        }
    }
}
