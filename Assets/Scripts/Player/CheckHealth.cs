using System;
using System.Collections;
using UnityEngine;

public class CheckHealth : MonoBehaviour
{
    public enum GameResult
    {
        Unknown,
        Win,
        Lose,
    }

    public event Action<GameResult> OnGameEnd = delegate { };

    public Transform Player;
    public Transform Enemy;
    public GameObject UILose;
    public GameObject UIWin;
    public Sprite[] Images;

    public float playerMaxHealth;
    public float enemyMaxHealth;
    [Space(10)]
    public float UIShowDelay = 2.0f;
    public float UIShowDuration = 0.5f;
    public float gameEndTimeScale = 0.1f;

    SpriteRenderer playerSprite;
    SpriteRenderer enemySprite;
    HealthSystem playerHealth;
    HealthSystem enemyHealth;
    bool END = false;

    [Range(0,100)] public static int odds;

    private void Start() {
        playerSprite = Player.Find("PlayerBody/Head/HeadSpriteAnchor/HeadSprite").GetComponent<SpriteRenderer>();
        enemySprite = Enemy.Find("EnemyBody/Head/HeadSpriteAnchor/HeadSprite").GetComponent<SpriteRenderer>();

        playerHealth = Player.GetComponentInChildren<HealthSystem>();
        enemyHealth = Enemy.GetComponentInChildren<HealthSystem>();
    }

    private void Update() {
        if (!END)
        {
            CheckPlayer();
            CheckAI();
        }else
        {
            CheckWiner();
        }
    }

    private void CheckWiner()
    {
        if (playerHealth.getCurrentHealth == 0)
        {
            Lose();
        }
        else if(enemyHealth.getCurrentHealth == 0)
        {
            Win();
        }
        else
        {
            Debug.Log("No Winner");
        }

    }

    private void CheckPlayer()
    {
        if (playerHealth.getCurrentHealth > playerMaxHealth * 0.5f)
        {
            playerSprite.sprite = Images[0];
        }
        else if(playerHealth.getCurrentHealth <= playerMaxHealth * 0.5 && playerHealth.getCurrentHealth > 0)
        {
            playerSprite.sprite = Images[1];
        }
        else
        {
            END = true;
        }
    }

    private void Lose()
    {
        if (PauseMenu.gameIsPause) return;

        OnGameEnd.Invoke(GameResult.Lose);

        playerSprite.sprite = Images[2];
        enemySprite.sprite = Images[3];

        StartCoroutine(GameEndCoroutine(UIShowDelay, UIShowDuration));
    }

    private void CheckAI()
    {
        if (enemyHealth.getCurrentHealth > 145)
        {
            if (enemyHealth.getCurrentHealth > 203)
                odds = 90;
            else 
            {
                odds = 75;
            }
            enemySprite.sprite = Images[3];
        }
        else if(enemyHealth.getCurrentHealth <= 145 && enemyHealth.getCurrentHealth > 0)
        {
            if (enemyHealth.getCurrentHealth > 87)
                odds = 68;
            else
            {
                odds = 60;
            }
            enemySprite.sprite = Images[4];
        }
        else
        {
            END = true;
        }
    }

    private void Win()
    {
        if (PauseMenu.gameIsPause) return;

        OnGameEnd.Invoke(GameResult.Win);

        playerSprite.sprite = Images[0];
        enemySprite.sprite = Images[5];

        StartCoroutine(GameEndCoroutine(UIShowDelay, UIShowDuration));
    }

    private IEnumerator GameEndCoroutine(float delay, float duration)
    {
        PauseMenu.gameIsPause = true;
        Time.timeScale = gameEndTimeScale;

        while (delay > 0)
        {
            delay -= Time.unscaledDeltaTime;
            yield return null;
        }
        UIWin.SetActive(true);
    }
}
