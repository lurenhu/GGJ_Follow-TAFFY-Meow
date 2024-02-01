using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject setMenu;
    public GameObject inputSettingMenu;
    public Slider ValueOne;
    public Slider ValueTwo;
    public AudioClip Botton;

    private void Awake()
    {
        ValueOne.value = PlayerPrefs.GetFloat("Theme (UnityEngine.AudioSource)");
        ValueTwo.value = PlayerPrefs.GetFloat("UISound (UnityEngine.AudioSource)");
    }

    public void PlayGame()
    {
        AudioCtrl.GetInstance.UISoundFunc(Botton);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);

        GameManager.Instance.startGameCounter++;
    }

    public void PlayMutiplayerGame()
    {
        AudioCtrl.GetInstance.UISoundFunc(Botton);
        Time.timeScale = 1;
        SceneManager.LoadScene(2);

        GameManager.Instance.startMutiplayModeCounter++;
    }

    public void QuitGame()
    {
        AudioCtrl.GetInstance.UISoundFunc(Botton);
        Application.Quit();
    }

    public void ReturnMenu()
    {
        AudioCtrl.GetInstance.UISoundFunc(Botton);
        setMenu.SetActive(false);
    }

    public void SetMenu()
    {
        AudioCtrl.GetInstance.UISoundFunc(Botton);
        setMenu.SetActive(true);
    }

    public void SetInputSettingMenu()
    {
        AudioCtrl.GetInstance.UISoundFunc(Botton); 
        inputSettingMenu.SetActive(true);
    }

    public void ReturnSetMenu()
    {
        AudioCtrl.GetInstance.UISoundFunc(Botton); 
        inputSettingMenu.SetActive(false);
    }

    public void SetSoundOne()
    {
        AudioCtrl.GetInstance.ChangeAudioVolume(VolumeType.kMusic , ValueOne.value);
        AudioCtrl.GetInstance.ChangeAudioVolume(VolumeType.kAmbience, ValueOne.value);
    }

    public void SetSoundTwo()
    {
        AudioCtrl.GetInstance.ChangeAudioVolume(VolumeType.kUISound, ValueTwo.value);
        AudioCtrl.GetInstance.ChangeAudioVolume(VolumeType.kGameSound, ValueTwo.value);
    }
}
