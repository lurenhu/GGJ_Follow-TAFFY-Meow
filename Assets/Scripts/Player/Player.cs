using System;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform bigJoint;
    Transform smallJoint;
    Transform fist;

    JointMotor2D bigJointMotor;
    JointMotor2D smallJointMotor;

    Rigidbody2D headRB2D;

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

    private void Awake() {
        Init();
    }

    private void Start() {
        bigJointMotor = bigJoint.GetComponent<HingeJoint2D>().motor;
        smallJointMotor = smallJoint.GetComponent<HingeJoint2D>().motor;

        bigJointMotor.maxMotorTorque = bigJointSpiningForce;
        smallJointMotor.maxMotorTorque = smallJointSpiningForce;

        headRB2D = head.GetComponent<Rigidbody2D>();
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

        if (!Input.anyKey)
        {
            bigJointMotor.motorSpeed = 0;
            bigJoint.GetComponent<HingeJoint2D>().motor = bigJointMotor;
            smallJointMotor.motorSpeed = 0;
            smallJoint.GetComponent<HingeJoint2D>().motor = smallJointMotor;
        }
    }

    private void PlayerHeadController()
    {
        if (Input.GetKey(KeyCode.F) && head.position.y < maxUp)
        {
            headRB2D.MovePosition(headRB2D.position + (Vector2.up * moveSpeed * Time.fixedDeltaTime));
        }

        if (Input.GetKey(KeyCode.J) && head.position.y > minDown)
        {
            headRB2D.MovePosition(headRB2D.position + (Vector2.down * moveSpeed * Time.fixedDeltaTime));
        }
    }

    private void PlayerArmController()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            bigJointMotor.motorSpeed = -bigJointSpiningSpeed;
            bigJoint.GetComponent<HingeJoint2D>().motor = bigJointMotor;
        }

        if (Input.GetKey(KeyCode.W))
        {
            bigJointMotor.motorSpeed = bigJointSpiningSpeed;
            bigJoint.GetComponent<HingeJoint2D>().motor = bigJointMotor;
        }

        if (Input.GetKey(KeyCode.O))
        {
            smallJointMotor.motorSpeed = -smallJointSpiningSpeed;
            smallJoint.GetComponent<HingeJoint2D>().motor = smallJointMotor;
        }

        if (Input.GetKey(KeyCode.P))
        {
            smallJointMotor.motorSpeed = smallJointSpiningSpeed;
            smallJoint.GetComponent<HingeJoint2D>().motor = smallJointMotor;
        }

        
    }

    


}
