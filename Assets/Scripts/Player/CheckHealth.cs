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
    [Range(0,100)] public static int odds;

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
        if (myHealth.getCurrentHealth > 145)
        {
            Head.sprite = Images[0];
        }
        else if(myHealth.getCurrentHealth <= 145 && myHealth.getCurrentHealth > 0)
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
        if (myHealth.getCurrentHealth > 145)
        {
            if (myHealth.getCurrentHealth > 203)
                odds = 90;
            else 
            {
                odds = 75;
            }
            Head.sprite = Images[3];
        }
        else if(myHealth.getCurrentHealth <= 145 && myHealth.getCurrentHealth > 0)
        {
            if (myHealth.getCurrentHealth > 87)
                odds = 68;
            else
            {
                odds = 60;
            }
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
