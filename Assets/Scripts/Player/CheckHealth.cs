using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckHealth : MonoBehaviour
{
    public enum GameResult
    {
        Unknown,
        Win,
        Lose,
    }

    public event Action<GameResult> OnGameEnd = delegate { };

    // public Transform Player_2;
    // public Transform Player;
    // public Transform Enemy;
    public GameObject UILose;
    public GameObject UIWin;
    public Sprite[] Images;

    public float playerMaxHealth;
    public float player2MaxHealth;
    public float enemyMaxHealth;

    [Space(10)]
    public float UIShowDelay = 2.0f;
    public float UIShowDuration = 0.5f;
    public float gameEndTimeScale = 0.1f;

    public SpriteRenderer playerSprite;
    public SpriteRenderer player2Sprite;
    public SpriteRenderer enemySprite;
    public HealthSystem playerHealth;
    public HealthSystem player2Health;
    public HealthSystem enemyHealth;
    bool END = false;

    [Range(0,100)] public static int odds;

    private void Start() {
        // playerSprite = Player.Find("PlayerBody/Head/HeadSpriteAnchor/HeadSprite").GetComponent<SpriteRenderer>();
        // player2Sprite = Player_2.Find("PlayerBody/Head/HeadSpriteAnchor/HeadSprite").GetComponent<SpriteRenderer>();
        // enemySprite = Enemy.Find("EnemyBody/Head/HeadSpriteAnchor/HeadSprite").GetComponent<SpriteRenderer>();

        // playerHealth = Player.GetComponentInChildren<HealthSystem>();
        // player2Health = Player_2.GetComponentInChildren<HealthSystem>();
        // enemyHealth = Enemy.GetComponentInChildren<HealthSystem>();
    }

    private void Update() {
        if (!END)
        {
            CheckPlayer();
            CheckPlayer_2();
            CheckAI();
        }else
        {
            CheckWiner();
        }
    }

    private void CheckWiner()
    {
        if (playerHealth.getCurrentHealth == 0 && enemyHealth.getCurrentHealth != 0)
        {
            Lose();
        }
        else if(enemyHealth.getCurrentHealth == 0 && playerHealth.getCurrentHealth != 0)
        {
            Win();
        }
        else if(playerHealth.getCurrentHealth == 0 && player2Health.getCurrentHealth != 0)
        {
            Debug.Log("Player1 win");
        }
        else if(player2Health.getCurrentHealth == 0 && playerHealth.getCurrentHealth != 0)
        {
            Debug.Log("Player2 win");
        }
        else
        {
            Debug.Log("No Winner");
        }

    }

    private void CheckPlayer()
    {
        if (playerHealth == null || playerSprite == null) return;

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

    private void CheckPlayer_2()
    {
        if (player2Health == null || player2Sprite == null) return;

        if (player2Health.getCurrentHealth > player2MaxHealth * 0.5f)
        {
            player2Sprite.sprite = Images[3];
        }
        else if(player2Health.getCurrentHealth <= player2MaxHealth * 0.5 && player2Health.getCurrentHealth > 0)
        {
            player2Sprite.sprite = Images[4];
        }
        else
        {
            END = true;
        }
    }

    private void CheckAI()
    {
        if (enemyHealth == null || enemySprite == null) return;

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

     private void Lose()
    {
        if (PauseMenu.gameIsPause) return;

        OnGameEnd.Invoke(GameResult.Lose);

        playerSprite.sprite = Images[2];
        enemySprite.sprite = Images[3];

        StartCoroutine(GameEndCoroutine(UIShowDelay, UIShowDuration, GameResult.Lose));
    }

    private void Win()
    {
        if (PauseMenu.gameIsPause) return;

        OnGameEnd.Invoke(GameResult.Win);

        playerSprite.sprite = Images[0];
        enemySprite.sprite = Images[5];

        StartCoroutine(GameEndCoroutine(UIShowDelay, UIShowDuration, GameResult.Win));
    }

    private IEnumerator GameEndCoroutine(float delay, float duration, GameResult gameResult)
    {
        PauseMenu.gameIsPause = true;
        Time.timeScale = gameEndTimeScale;

        while (delay > 0)
        {
            delay -= Time.unscaledDeltaTime;
            yield return null;
        }

        if (gameResult == GameResult.Win)
        {
            UIWin.SetActive(true);
        }
        if (gameResult == GameResult.Lose)
        {
            UILose.SetActive(true);
        }
        
    }
}
