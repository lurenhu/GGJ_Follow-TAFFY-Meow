using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public enum TraceMode
    {
        Disable,
        Keep,
        MouseLeft,
        MouseRight
    }

    public GameObject target;
    public Rigidbody2D source;
    public TraceMode traceMode = TraceMode.Disable;
    [Range(0.0f, 100000.0f)]
    public float traceAcc = 200.0f;


    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!target || !source) return;

        bool trace = false;

        if (traceMode == TraceMode.MouseLeft)
        {
            if (Input.GetMouseButton(0))
            {
                trace = true;
            }
        }
        if (traceMode == TraceMode.MouseRight)
        {
            if (Input.GetMouseButton(1))
            {
                trace = true;
            }
        }
        if (traceMode == TraceMode.Keep)
        {
            trace = true;
        }


        if(trace)
        {
            Vector3 vectorToTarget = target.transform.position - source.transform.position;
            source.AddForce(
                new Vector2(
                    vectorToTarget.x * traceAcc * Time.fixedDeltaTime,
                    vectorToTarget.y * traceAcc * Time.fixedDeltaTime
                )
            );
        }
    }

}
