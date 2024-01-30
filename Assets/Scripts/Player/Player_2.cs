using System;
using UnityEngine;
using UnityEngine.UI;

public class Player_2 : MonoBehaviour
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
        if (Input.GetKey(GameManager.Instance.Player2_Head_Up) && head.localPosition.y < maxUp)
        {
            UIInput.Find("Key_F/Panel").gameObject.SetActive(true);

            head.transform.position += new Vector3(0,moveSpeed * Time.deltaTime,0);
        }

        if (Input.GetKey(GameManager.Instance.Player2_Head_Down) && head.localPosition.y > minDown)
        {
            UIInput.Find("Key_J/Panel").gameObject.SetActive(true);

            head.transform.position += new Vector3(0,-moveSpeed * Time.deltaTime,0);
        }
    }

    /// <summary>
    /// 角色手臂控制
    /// </summary>
    private void PlayerArmController()
    {
        if (Input.GetKey(GameManager.Instance.Player2_BigArm_Up))
        {
            UIInput.Find("Key_Q/Panel").gameObject.SetActive(true);

            bigJointMotor.motorSpeed = -bigJointSpiningSpeed;
            bigJoint.GetComponent<HingeJoint2D>().motor = bigJointMotor;
        }

        if (Input.GetKey(GameManager.Instance.Player2_BigArm_Down))
        {
            UIInput.Find("Key_W/Panel").gameObject.SetActive(true);

            bigJointMotor.motorSpeed = bigJointSpiningSpeed;
            bigJoint.GetComponent<HingeJoint2D>().motor = bigJointMotor;
        }

        if (Input.GetKey(GameManager.Instance.Player2_SmallArm_Up))
        {
            UIInput.Find("Key_O/Panel").gameObject.SetActive(true);

            smallJointMotor.motorSpeed = -smallJointSpiningSpeed;
            smallJoint.GetComponent<HingeJoint2D>().motor = smallJointMotor;
        }

        if (Input.GetKey(GameManager.Instance.Player2_SmallArm_Down))
        {
            UIInput.Find("Key_P/Panel").gameObject.SetActive(true);

            smallJointMotor.motorSpeed = smallJointSpiningSpeed;
            smallJoint.GetComponent<HingeJoint2D>().motor = smallJointMotor;
        }

        
    }

    /// <summary>
    /// 定义初始状态
    /// </summary>
    private void ReturnInitialState()
    {
        if (!Input.GetKey(GameManager.Instance.Player2_Head_Up) && !Input.GetKey(GameManager.Instance.Player2_Head_Down))
        {
            UIInput.Find("Key_F/Panel").gameObject.SetActive(false);
            UIInput.Find("Key_J/Panel").gameObject.SetActive(false);
        }
        
        if (!Input.GetKey(GameManager.Instance.Player2_BigArm_Up) && !Input.GetKey(GameManager.Instance.Player2_BigArm_Down))
        {
            UIInput.Find("Key_Q/Panel").gameObject.SetActive(false);
            UIInput.Find("Key_W/Panel").gameObject.SetActive(false);

            bigJointMotor.motorSpeed = 0;
            bigJoint.GetComponent<HingeJoint2D>().motor = bigJointMotor;
        }

        if (!Input.GetKey(GameManager.Instance.Player2_SmallArm_Up) && !Input.GetKey(GameManager.Instance.Player2_SmallArm_Down))
        {
            UIInput.Find("Key_O/Panel").gameObject.SetActive(false);
            UIInput.Find("Key_P/Panel").gameObject.SetActive(false);

            smallJointMotor.motorSpeed = 0;
            smallJoint.GetComponent<HingeJoint2D>().motor = smallJointMotor;
        }
    }

    


}
