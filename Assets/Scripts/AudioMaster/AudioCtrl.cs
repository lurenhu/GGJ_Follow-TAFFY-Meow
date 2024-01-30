using ClockStone;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    [Header("死亡音效")]
    private AudioClip deadClip;
    [SerializeField]
    [Header("移动音效")]
    public AudioClip moveClip;
    [SerializeField]
    [Header("玩家拳头刚体")]
    private Rigidbody2D playerFistRb;
    [SerializeField]
    [Header("敌人拳头刚体")]
    private Rigidbody2D enemyFistRb;
    [SerializeField]
    [Header("玩家的头刚体")]
    private Rigidbody2D playerHeadRb;
    [SerializeField]
    [Header("敌人的头的刚体")]
    private Rigidbody2D enemyHeadRb;
    [SerializeField]
    private bool isStop = false;

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
            audioVolume[i] = PlayerPrefs.GetFloat(audioSource[i].ToString());
            Debug.Log(audioSource[i].ToString());
            ChangeAudioVolume((VolumeType)i, audioVolume[i]);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            try
            {
                var tmp1 = playerFistRb;
                var tmp2 = playerFistRb.velocity.magnitude;
            }
            catch (Exception e) 
            {
                GameObject.Find("Enemy/EnemyArm/Fist").TryGetComponent<Rigidbody2D>(out playerFistRb);
                GameObject.Find("Player/PlayerArm/Fist").TryGetComponent<Rigidbody2D>(out enemyFistRb);

                if (EffectsManager.Instance != null)
                {
                    EffectsManager.Instance.OnHitEvent += AttackSoundFunc;
                }
                PlayerMoveSound();
                EnemyMoveSound();
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (isStop == false)
                {
                    audioSource[audioSource.Length - 2].Pause();
                    audioSource[audioSource.Length - 1].Pause();
                    isStop = true;
                    return;
                }
                else
                {
                    isStop = false;
                }

            }
        }
        else
        {
            audioSource[audioSource.Length - 2].Pause();
            audioSource[audioSource.Length - 1].Pause();
            return;
        }
        if(isStop==false)
        {
            if (playerFistRb != null)
            {
                if (playerFistRb.velocity.magnitude < 5.0f)
                {
                    audioSource[audioSource.Length - 1].Pause();
                }
                else
                {
                    audioSource[audioSource.Length - 1].UnPause();
                }
            }
            if (enemyFistRb != null)
            {
                if (enemyFistRb.velocity.magnitude < 5.0f)
                {
                    audioSource[audioSource.Length - 2].Pause();
                }
                else
                {
                    audioSource[audioSource.Length - 2].UnPause();
                }
            }
        }
    }

    public void ChangeAudioVolume(VolumeType volumeType, float volume)
    {
        this.audioVolume[(int)volumeType] = volume;
        audioSource[(int)volumeType].volume = this.audioVolume[(int)volumeType];
        PlayerPrefs.SetFloat(audioSource[(int)volumeType].ToString(), volume);
        if (volumeType == VolumeType.kGameSound)
        {
            audioSource[audioSource.Length - 1].volume = audioVolume[(int)volumeType]
                * gameSoundVolume[(int)GameSoundType.kMove];
            audioSource[audioSource.Length - 2].volume = audioVolume[(int)volumeType]
                * gameSoundVolume[(int)GameSoundType.kMove];
        }
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
            if (gameSoundType != GameSoundType.kMove)
            {
                audioSource[(int)volumeType].pitch = UnityEngine.Random.Range(Mathf.Min(randomPitchMin, randomPitchMax),
    Mathf.Max(randomPitchMin, randomPitchMax));
                float lastVolume = audioSource[(int)volumeType].volume;
                audioSource[(int)volumeType].volume = lastVolume * gameSoundVolume[(int)gameSoundType];
                Debug.Log(gameSoundVolume[(int)gameSoundType]);
                audioSource[(int)volumeType].PlayOneShot(audioClip);
                audioSource[(int)volumeType].volume = lastVolume;
            }
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
            PlaySound(VolumeType.kGameSound, hitClip[UnityEngine.Random.Range(0, hitClip.Length)], GameSoundType.kHit);
        }
        else
        {
            Debug.Log("播放收击");
            PlaySound(VolumeType.kGameSound, attackClip[UnityEngine.Random.Range(0, attackClip.Length)], GameSoundType.kAttack);
        }
    }

    public void PlayDeadSound()
    {
        PlaySound(VolumeType.kGameSound, deadClip, GameSoundType.kDeed);
        audioSource[audioSource.Length - 1].Pause();
        audioSource[audioSource.Length - 2].Pause();
    }

    public void PlayerMoveSound()
    {
        if (playerFistRb != null)
        {
            audioSource[audioSource.Length - 1].clip = moveClip;
            audioSource[audioSource.Length - 1].loop = true;
            audioSource[audioSource.Length - 1].Play();
        }

    }
    public void EnemyMoveSound()
    {
        if(enemyFistRb != null)
        {
            audioSource[audioSource.Length - 2].clip = moveClip;
            audioSource[audioSource.Length - 2].loop = true;
            audioSource[audioSource.Length - 2].Play();
        }
    }

    public void SetStart()
    {
        isStop = false;
    }
}
