using System.Threading;
using JetBrains.Annotations;
using UnityEngine;


[System.Serializable]
public class EnemyHeadBlackborad : Blackborad
{
    public Transform enemyHead;

    [Space(5)]
    [Header("头部移动范围及移动速度")]
    public float maxUp;
    public float minDown;
    public float moveSpeed;

    [Space(5)]
    [Header("闲置时间")]
    public float idleTime;
}

public class AI_MoveUp : IState
{
    FSM fsm;
    EnemyHeadBlackborad enemyHeadBlackborad;

    Rigidbody2D headRB2D;

    public AI_MoveUp(FSM fsm)
    {
        this.fsm = fsm;
        this.enemyHeadBlackborad = fsm.blackborad as EnemyHeadBlackborad;
    }
    public void OnCheck()
    {
    }

    public void OnEnter()
    {
        headRB2D = enemyHeadBlackborad.enemyHead.GetComponent<Rigidbody2D>();
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        if (enemyHeadBlackborad.enemyHead.localPosition.y >= enemyHeadBlackborad.maxUp)
        {
            fsm.SwitchState(StateType.MOVEDOWN);
            return;
        }

        headRB2D.MovePosition(headRB2D.position + (Vector2.up * enemyHeadBlackborad.moveSpeed * Time.deltaTime));
    }
}

public class AI_MoveDown : IState
{
    FSM fsm;
    EnemyHeadBlackborad enemyHeadBlackborad;

    Rigidbody2D headRB2D;
    public AI_MoveDown(FSM fsm)
    {
        this.fsm = fsm;
        this.enemyHeadBlackborad = fsm.blackborad as EnemyHeadBlackborad;
    }
    public void OnCheck()
    {
    }

    public void OnEnter()
    {
        headRB2D = enemyHeadBlackborad.enemyHead.GetComponent<Rigidbody2D>();
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        if (enemyHeadBlackborad.enemyHead.localPosition.y <= enemyHeadBlackborad.minDown)
        {
            fsm.SwitchState(StateType.MOVEUP);
            return;
        }

        headRB2D.MovePosition(headRB2D.position + (Vector2.down * enemyHeadBlackborad.moveSpeed * Time.deltaTime));
    }
}

public class AI_Idle : IState
{
    FSM fsm;
    EnemyHeadBlackborad enemyHeadBlackborad;

    float timer;

    public AI_Idle(FSM fsm)
    {
        this.fsm = fsm;
        this.enemyHeadBlackborad = fsm.blackborad as EnemyHeadBlackborad;
    }

    
    public void OnCheck()
    {

    }

    public void OnEnter()
    {
        timer = 0;
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer > enemyHeadBlackborad.idleTime)
        {
            fsm.SwitchState(StateType.MOVE);
        }
    }
}

public class AI_Move : IState
{
    FSM fsm;
    EnemyHeadBlackborad enemyHeadBlackborad;

    Vector2 targetPosition;
    Rigidbody2D headRB2D;

    public AI_Move(FSM fsm)
    {
        this.fsm = fsm;
        this.enemyHeadBlackborad = fsm.blackborad as EnemyHeadBlackborad;
    }

    
    public void OnCheck()
    {
    }

    public void OnEnter()
    {
        headRB2D = enemyHeadBlackborad.enemyHead.GetComponent<Rigidbody2D>();
        float RandomY = Random.Range(enemyHeadBlackborad.minDown,enemyHeadBlackborad.maxUp); 
        targetPosition = new Vector2(enemyHeadBlackborad.enemyHead.localPosition.x,RandomY);
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        if (Vector2.Distance(targetPosition,enemyHeadBlackborad.enemyHead.localPosition) < 0.1f)
        {
            fsm.SwitchState(StateType.IDLE);
        }else
        {
            if (targetPosition.y > enemyHeadBlackborad.enemyHead.localPosition.y)
            {
                headRB2D.MovePosition(headRB2D.position + (Vector2.up * enemyHeadBlackborad.moveSpeed * Time.deltaTime));
            }else
            {   
                headRB2D.MovePosition(headRB2D.position + (Vector2.down * enemyHeadBlackborad.moveSpeed * Time.deltaTime));
            }
        }

    }
}



public class EnemyHead : MonoBehaviour
{
    private FSM fsm;
    public EnemyHeadBlackborad enemyHeadBlackborad;

    private void Start() {
        fsm = new FSM(enemyHeadBlackborad);
        //fsm.AddState(StateType.MOVEDOWN,new AI_MoveDown(fsm));
        //fsm.AddState(StateType.MOVEUP,new AI_MoveUp(fsm));

        fsm.AddState(StateType.MOVE,new AI_Move(fsm));
        fsm.AddState(StateType.IDLE,new AI_Idle(fsm));

        fsm.SwitchState(StateType.MOVE);
        
    }

    private void Update() {
        fsm.OnUpdate();
    }

    private void FixedUpdate() {
        fsm.OnFixedUpdate();
    }
}
