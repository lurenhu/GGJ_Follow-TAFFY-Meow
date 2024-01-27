using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;

public class CheckHealth : MonoBehaviour
{
    public bool isPlayer;
    public HealthSystem myHealth;
    public SpriteRenderer Head;

    public GameObject UILose;
    public GameObject UIWin;

    public Sprite[] Images;

    private void Update() {
        if (isPlayer)
        {
            CheckPlayer();
        }else
        {
            CheckAI();
        }
    }

    private void CheckPlayer()
    {
        if (myHealth.getCurrentHealth > 50)
        {
            Head.sprite = Images[0];
        }
        else if(myHealth.getCurrentHealth <= 50 && myHealth.getCurrentHealth > 0)
        {
            Head.sprite = Images[1];
        }
        else
        {
            Head.sprite = Images[2];
            PauseMenu.gameIsPause = true;
            Time.timeScale = 0;
            UILose.SetActive(true);
        }
    }

    private void CheckAI()
    {
        if (myHealth.getCurrentHealth > 50)
        {
            Head.sprite = Images[3];
        }
        else if(myHealth.getCurrentHealth <= 50 && myHealth.getCurrentHealth > 0)
        {
            Head.sprite = Images[4];
        }
        else
        {
            Head.sprite = Images[5];
            PauseMenu.gameIsPause = true;
            Time.timeScale = 0;
            UIWin.SetActive(true);
        }
    }
}
