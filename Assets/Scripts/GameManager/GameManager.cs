using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{

    [Space(10)]
    [Header("Player_1 Input")]
    public KeyCode Player1_BigArm_Up;
    public KeyCode Player1_BigArm_Down;
    public KeyCode Player1_SmallArm_Up;
    public KeyCode Player1_SmallArm_Down;
    public KeyCode Player1_Head_Up;
    public KeyCode Player1_Head_Down;

    [Space(10)]
    [Header("Player_2 Input")]
    public KeyCode Player2_BigArm_Up;
    public KeyCode Player2_BigArm_Down;
    public KeyCode Player2_SmallArm_Up;
    public KeyCode Player2_SmallArm_Down;
    public KeyCode Player2_Head_Up;
    public KeyCode Player2_Head_Down;

    public int startGameCounter;
    public int startMutiplayModeCounter;

    protected override void Awake()
    {
        base.Awake();

        //--- Player1 ---
        Player1_BigArm_Up = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player1_BigArm_Up_Key","W"));
        Player1_BigArm_Down = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player1_BigArm_Down_Key","Q"));
        Player1_SmallArm_Up = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player1_SmallArm_Up_Key","T"));
        Player1_SmallArm_Down = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player1_SmallArm_Down_Key","Y"));
        Player1_Head_Up = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player1_Head_Up_Key","D"));
        Player1_Head_Down = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player1_Head_Down_Key","F"));

        //--- Player2 ---
        Player2_BigArm_Up = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player2_BigArm_Up_Key","O"));
        Player2_BigArm_Down = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player2_BigArm_Down_Key","P"));
        Player2_SmallArm_Up = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player2_SmallArm_Up_Key",KeyCode.Backslash.ToString()));
        Player2_SmallArm_Down = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player2_SmallArm_Down_Key",KeyCode.RightBracket.ToString()));
        Player2_Head_Up = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player2_Head_Up_Key",KeyCode.Semicolon.ToString()));
        Player2_Head_Down = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Player2_Head_Down_Key",KeyCode.Quote.ToString()));

    }
}
