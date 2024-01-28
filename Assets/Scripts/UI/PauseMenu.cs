using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPause = false;
    public GameObject pauseMenuUI;
    public AudioClip Botton;

    public Transform panel;

    public GameObject Tutorial;

    public AudioClip readyClip;
    public AudioClip goClip;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReadyGo());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    private IEnumerator ReadyGo()
    {
        Time.timeScale = 0;
        gameIsPause = true;

        Text readyGo = panel.GetComponentInChildren<Text>();

        readyGo.text = "Ready";
        AudioCtrl.GetInstance.PlaySound(VolumeType.kUISound,readyClip);
        yield return new WaitForSecondsRealtime(1f);
        readyGo.text = "Go";
        AudioCtrl.GetInstance.PlaySound(VolumeType.kUISound,goClip);
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1;
        gameIsPause = false;
        panel.gameObject.SetActive(false);
    }

    public void Resume()
    {
        AudioCtrl.GetInstance.UISoundFunc(Botton);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void MainMenu()
    {
        AudioCtrl.GetInstance.UISoundFunc(Botton);
        gameIsPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        AudioCtrl.GetInstance.UISoundFunc(Botton);
        gameIsPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        AudioCtrl.GetInstance.UISoundFunc(Botton);
        Application.Quit();
    }

    public void OpenTutorial()
    {
        Tutorial.SetActive(true);
    }

    public void CloseTutorial()
    {
        Tutorial.SetActive(false);
    }
}
