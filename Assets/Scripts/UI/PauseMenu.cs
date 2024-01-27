using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPause = false;
    public GameObject pauseMenuUI;
    public AudioClip Botton;
    // Start is called before the first frame update
    void Start()
    {
        
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
