using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class InputMenu : MonoBehaviour
{
    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;

    private void Start() {
        menuPanel = transform.Find("InputMenu");
        menuPanel.gameObject.SetActive(false);
        waitingForKey = false;

        for (int i = 0; i < menuPanel.childCount; i++)
        {
            if (menuPanel.GetChild(i).name == "Player1_BigArmUp_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player1_BigArm_Up.ToString();
            if (menuPanel.GetChild(i).name == "Player1_BigArmDown_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player1_BigArm_Down.ToString(); 
            if (menuPanel.GetChild(i).name == "Player1_SmallArmUp_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player1_SmallArm_Up.ToString(); 
            if (menuPanel.GetChild(i).name == "Player1_SmallArmDown_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player1_SmallArm_Down.ToString(); 
            if (menuPanel.GetChild(i).name == "Player1_HeadUp_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player1_Head_Up.ToString(); 
            if (menuPanel.GetChild(i).name == "Player1_HeadDown_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player1_Head_Down.ToString(); 
            if (menuPanel.GetChild(i).name == "Player2_BigArmUp_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player2_BigArm_Up.ToString(); 
            if (menuPanel.GetChild(i).name == "Player2_BigArmDown_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player2_BigArm_Down.ToString(); 
            if (menuPanel.GetChild(i).name == "Player2_SmallArmDown_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player2_SmallArm_Up.ToString(); 
            if (menuPanel.GetChild(i).name == "Player2_SmallArmUp_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player2_SmallArm_Down.ToString(); 
            if (menuPanel.GetChild(i).name == "Player2_HeadDown_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player2_Head_Up.ToString(); 
            if (menuPanel.GetChild(i).name == "Player2_HeadUp_Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.Instance.Player2_Head_Down.ToString();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab) && menuPanel.gameObject.activeSelf)
        {
            PauseMenu.gameIsPause = false;
            Time.timeScale = 1;
            menuPanel.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !menuPanel.gameObject.activeSelf)
        {
            PauseMenu.gameIsPause = true;
            Time.timeScale = 0;
            menuPanel.gameObject.SetActive(true);
        }
    }

    private void OnGUI() {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        while(!keyEvent.isKey)
            yield return null;
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName)
        {
        case "Player1_BigArm_Up_Key":
            GameManager.Instance.Player1_BigArm_Up = newKey;
            buttonText.text = GameManager.Instance.Player1_BigArm_Up.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player1_BigArm_Up.ToString());
            break;
        case "Player1_BigArm_Down_Key":
            GameManager.Instance.Player1_BigArm_Down = newKey;
            buttonText.text = GameManager.Instance.Player1_BigArm_Down.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player1_BigArm_Down.ToString());
            break;
        case "Player1_SmallArm_Up_Key":
            GameManager.Instance.Player1_SmallArm_Up = newKey;
            buttonText.text = GameManager.Instance.Player1_SmallArm_Up.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player1_SmallArm_Up.ToString());
            break;
        case "Player1_SmallArm_Down_Key":
            GameManager.Instance.Player1_SmallArm_Down = newKey;
            buttonText.text = GameManager.Instance.Player1_SmallArm_Down.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player1_SmallArm_Down.ToString());
            break;
        case "Player1_Head_Up_Key":
            GameManager.Instance.Player1_Head_Up = newKey;
            buttonText.text = GameManager.Instance.Player1_Head_Up.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player1_Head_Up.ToString());
            break;
        case "Player1_Head_Down_Key":
            GameManager.Instance.Player1_Head_Down = newKey;
            buttonText.text = GameManager.Instance.Player1_Head_Down.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player1_Head_Down.ToString());
            break;
        case "Player2_BigArm_Up_Key":
            GameManager.Instance.Player2_BigArm_Up = newKey;
            buttonText.text = GameManager.Instance.Player2_BigArm_Up.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player2_BigArm_Up.ToString());
            break;
        case "Player2_BigArm_Down_Key":
            GameManager.Instance.Player2_BigArm_Down = newKey;
            buttonText.text = GameManager.Instance.Player2_BigArm_Down.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player2_BigArm_Down.ToString());
            break;
        case "Player2_SmallArm_Up_Key":
            GameManager.Instance.Player2_SmallArm_Up = newKey;
            buttonText.text = GameManager.Instance.Player2_SmallArm_Up.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player2_SmallArm_Up.ToString());
            break;
        case "Player2_SmallArm_Down_Key":
            GameManager.Instance.Player2_SmallArm_Down = newKey;
            buttonText.text = GameManager.Instance.Player2_SmallArm_Down.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player2_SmallArm_Down.ToString());
            break;
        case "Player2_Head_Up_Key":
            GameManager.Instance.Player2_Head_Up = newKey;
            buttonText.text = GameManager.Instance.Player2_Head_Up.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player2_Head_Up.ToString());
            break;
        case "Player2_Head_Down_Key":
            GameManager.Instance.Player2_Head_Down = newKey;
            buttonText.text = GameManager.Instance.Player2_Head_Down.ToString();
            PlayerPrefs.SetString("Player1_BigArm_Up_Key",GameManager.Instance.Player2_Head_Down.ToString());
            break;

        }

        yield return null;
    }
}
