using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.MPE;
using UnityEngine;

public class CheckHealth : MonoBehaviour
{
    public bool isPlayer;
    public HealthSystem myHealth;
    public SpriteRenderer playerHead;
    public SpriteRenderer enemyHead;

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
            playerHead.sprite = Images[0];
        }
        else if(myHealth.getCurrentHealth <= 145 && myHealth.getCurrentHealth > 0)
        {
            playerHead.sprite = Images[1];
        }
        else
        {
            playerHead.sprite = Images[2];
            enemyHead.sprite = Images[3];

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
            enemyHead.sprite = Images[3];
        }
        else if(myHealth.getCurrentHealth <= 145 && myHealth.getCurrentHealth > 0)
        {
            if (myHealth.getCurrentHealth > 87)
                odds = 68;
            else
            {
                odds = 60;
            }
            enemyHead.sprite = Images[4];
        }
        else
        {
            enemyHead.sprite = Images[5];
            playerHead.sprite = Images[0];
            PauseMenu.gameIsPause = true;
            Time.timeScale = 0;
            UIWin.SetActive(true);
        }
    }
}
