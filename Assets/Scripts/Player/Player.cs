using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Transform bigJoint;
    Transform smallJoint;
    Transform fist;

    JointMotor2D bigJointMotor;
    JointMotor2D smallJointMotor;


    [Space(10)]
    [Header("头部及其移动速度")]
    [SerializeField] Transform head;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxUp;
    [SerializeField] float minDown;

    [Space(10)]
    [Header("关节旋转速度")]
    [SerializeField] float bigJointSpiningSpeed;
    [SerializeField] float smallJointSpiningSpeed;

    [Space(10)]
    [Header("关节旋转加速度")]
    [SerializeField] float bigJointSpiningForce;
    [SerializeField] float smallJointSpiningForce;

    [Space(10)]
    [SerializeField] Transform UIInput;

    private void Awake() {
        Init();
    }

    private void Start() {
        bigJointMotor = bigJoint.GetComponent<HingeJoint2D>().motor;
        smallJointMotor = smallJoint.GetComponent<HingeJoint2D>().motor;

        bigJointMotor.maxMotorTorque = bigJointSpiningForce;
        smallJointMotor.maxMotorTorque = smallJointSpiningForce;

    }

    private void Init()
    {
        bigJoint = transform.Find("BigArm");
        smallJoint = transform.Find("SmallArm");
        fist = transform.Find("Fist");
    }

    private void FixedUpdate() {
        PlayerArmController();
        
        PlayerHeadController();

        ReturnInitialState();
    }

    /// <summary>
    /// 角色头部控制
    /// </summary>
    private void PlayerHeadController()
    {
        if (Input.GetKey(GameManager.Instance.Player1_Head_Up) && head.localPosition.y < maxUp)
        {
            if (UIInput != null)
            {
                UIInput.Find("Key_HeadUp/Panel").gameObject.SetActive(true);
            }

            head.transform.position += new Vector3(0,moveSpeed * Time.deltaTime,0);
        }

        if (Input.GetKey(GameManager.Instance.Player1_Head_Down) && head.localPosition.y > minDown)
        {
            if (UIInput != null)
            {
                UIInput.Find("Key_HeadDown/Panel").gameObject.SetActive(true);
            }

            head.transform.position += new Vector3(0,-moveSpeed * Time.deltaTime,0);
        }
    }

    /// <summary>
    /// 角色手臂控制
    /// </summary>
    private void PlayerArmController()
    {
        
        if (PauseMenu.gameIsPause) return;
        
        if (Input.GetKey(GameManager.Instance.Player1_BigArm_Up))
        {
            if (UIInput != null)
            {
                UIInput.Find("Key_BigArmUp/Panel").gameObject.SetActive(true);
            }

            bigJointMotor.motorSpeed = -bigJointSpiningSpeed;
            bigJoint.GetComponent<HingeJoint2D>().motor = bigJointMotor;
        }

        if (Input.GetKey(GameManager.Instance.Player1_BigArm_Down))
        {
            if (UIInput != null)
            {
                UIInput.Find("Key_BigArmDown/Panel").gameObject.SetActive(true);
            }

            bigJointMotor.motorSpeed = bigJointSpiningSpeed;
            bigJoint.GetComponent<HingeJoint2D>().motor = bigJointMotor;
        }

        if (Input.GetKey(GameManager.Instance.Player1_SmallArm_Up))
        {
            if (UIInput != null)
            {
                UIInput.Find("Key_SmallArmUp/Panel").gameObject.SetActive(true);
            }

            smallJointMotor.motorSpeed = -smallJointSpiningSpeed;
            smallJoint.GetComponent<HingeJoint2D>().motor = smallJointMotor;
        }

        if (Input.GetKey(GameManager.Instance.Player1_SmallArm_Down))
        {
            if (UIInput != null)
            {
                UIInput.Find("Key_SmallArmDown/Panel").gameObject.SetActive(true);
            }

            smallJointMotor.motorSpeed = smallJointSpiningSpeed;
            smallJoint.GetComponent<HingeJoint2D>().motor = smallJointMotor;
        }

        
    }

    /// <summary>
    /// 定义初始状态
    /// </summary>
    private void ReturnInitialState()
    {
        if (UIInput != null)
        {
            if (!Input.GetKey(GameManager.Instance.Player1_Head_Up)) 
            {
                UIInput.Find("Key_HeadUp/Panel").gameObject.SetActive(false);
            }
            if (!Input.GetKey(GameManager.Instance.Player1_Head_Down))
            {
                UIInput.Find("Key_HeadDown/Panel").gameObject.SetActive(false);
            }
            
            if (!Input.GetKey(GameManager.Instance.Player1_BigArm_Up))
            {
                UIInput.Find("Key_BigArmUp/Panel").gameObject.SetActive(false);
            }
            if (!Input.GetKey(GameManager.Instance.Player1_BigArm_Down))
            {
                UIInput.Find("Key_BigArmDown/Panel").gameObject.SetActive(false);
            }
    
            if (!Input.GetKey(GameManager.Instance.Player1_SmallArm_Up))
            {
                UIInput.Find("Key_SmallArmUp/Panel").gameObject.SetActive(false);
            }
            if (!Input.GetKey(GameManager.Instance.Player1_SmallArm_Down))
            {
                UIInput.Find("Key_SmallArmDown/Panel").gameObject.SetActive(false);
            }
        }

        if (!Input.GetKey(GameManager.Instance.Player1_BigArm_Up) && !Input.GetKey(GameManager.Instance.Player1_BigArm_Down))
        {
            bigJointMotor.motorSpeed = 0;
            bigJoint.GetComponent<HingeJoint2D>().motor = bigJointMotor;
        }

        
        if (!Input.GetKey(GameManager.Instance.Player1_SmallArm_Up) && !Input.GetKey(GameManager.Instance.Player1_SmallArm_Down))
        {
            smallJointMotor.motorSpeed = 0;
            smallJoint.GetComponent<HingeJoint2D>().motor = smallJointMotor;
        }
    }

    


}
