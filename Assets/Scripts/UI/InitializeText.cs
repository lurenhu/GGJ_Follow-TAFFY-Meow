using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeText : MonoBehaviour
{
    [SerializeField] Text BigArmUp;
    [SerializeField] Text BigArmDown;
    [SerializeField] Text SmallArmUp;
    [SerializeField] Text SmallArmDown;
    [SerializeField] Text HeadUp;
    [SerializeField] Text HeadDown;

    private void Start() {
        if (BigArmUp.text != GameManager.Instance.Player1_BigArm_Up.ToString())
        {
            BigArmUp.text = GameManager.Instance.Player1_BigArm_Up.ToString();
        }
        if (BigArmDown.text != GameManager.Instance.Player1_BigArm_Down.ToString())
        {
            BigArmDown.text = GameManager.Instance.Player1_BigArm_Down.ToString();
        }
        if (SmallArmUp.text != GameManager.Instance.Player1_SmallArm_Up.ToString())
        {
            SmallArmUp.text = GameManager.Instance.Player1_SmallArm_Up.ToString();
        }
        if (SmallArmDown.text != GameManager.Instance.Player1_SmallArm_Down.ToString())
        {
            SmallArmDown.text = GameManager.Instance.Player1_SmallArm_Down.ToString();
        }
        if (HeadUp.text != GameManager.Instance.Player1_Head_Up.ToString())
        {
            HeadUp.text = GameManager.Instance.Player1_Head_Up.ToString();
        }
        if (HeadDown.text != GameManager.Instance.Player1_Head_Down.ToString())
        {
            HeadDown.text = GameManager.Instance.Player1_Head_Down.ToString();
        }
    }

}
