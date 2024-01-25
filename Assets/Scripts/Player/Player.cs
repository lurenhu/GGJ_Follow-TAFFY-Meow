using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform bigJoint;
    Transform smallJoint;
    Transform fist;
    
    [Range(50,100)] public float spiningSpeed;
    [Range(0,90)] public float SpiningUpMaxAngle;
    [Range(270,360)] public float SpiningDownMinAngle;

    Health health;

    private void Awake() {
        health = GetComponent<Health>();

        Init();
    }

    private void Init()
    {
        bigJoint = transform.Find("BigJoint");
        smallJoint = transform.Find("BigJoint/BigArm/SmallJoint");
        fist = transform.Find("BigJoint/BigArm/SmallJoint/SmallArm/Fist");
    }

    private void Update() {
        
        if (Input.GetKey(KeyCode.Q))
        {
            if (bigJoint.eulerAngles.z > 270 && bigJoint.eulerAngles.z < 360)
            {
                if (bigJoint.eulerAngles.z < SpiningDownMinAngle)
                {
                    return;
                }
                RotatingBigArmDown(spiningSpeed);
            }
            RotatingBigArmDown(spiningSpeed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (bigJoint.eulerAngles.z > 0 && bigJoint.eulerAngles.z < 90)
            {
                if (bigJoint.eulerAngles.z > SpiningUpMaxAngle)
                {
                    return;
                }
                RotatingBigArmUp(spiningSpeed);
            }
            RotatingBigArmUp(spiningSpeed);
        }

        if (Input.GetKey(KeyCode.O))
        {
            RotatingSmallArmDown(spiningSpeed);
        }

        if (Input.GetKey(KeyCode.P))
        {
            RotatingSmallArmUp(spiningSpeed);
        }
    }

    private void FixedUpdate() {
        //Debug.Log(fist.GetComponent<Rigidbody2D>().velocity);
    }

    private void RotatingBigArmUp(float spiningSpeed)
    {
        bigJoint.transform.Rotate(new Vector3(0,0,spiningSpeed*Time.deltaTime));
    }

    private void RotatingBigArmDown(float spiningSpeed)
    {
        bigJoint.transform.Rotate(new Vector3(0,0,-spiningSpeed*Time.deltaTime));
    }

    private void RotatingSmallArmUp(float spiningSpeed)
    {
        smallJoint.transform.Rotate(new Vector3(0,0,spiningSpeed*Time.deltaTime));
    }

    private void RotatingSmallArmDown(float spiningSpeed)
    {
        smallJoint.transform.Rotate(new Vector3(0,0,-spiningSpeed*Time.deltaTime));
    }
}
