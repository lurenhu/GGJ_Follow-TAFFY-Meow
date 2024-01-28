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
        Debug.Log("Ready");
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("waitfor 1s");
        readyGo.text = "Go";
        Debug.Log("Go");
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("waitfor 1s");

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
}
