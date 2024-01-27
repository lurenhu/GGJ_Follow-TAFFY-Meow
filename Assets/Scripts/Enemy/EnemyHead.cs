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

public class EnemyHead : MonoBehaviour
{
    private FSM fsm;
    public EnemyHeadBlackborad enemyHeadBlackborad;

    private void Start() {
        fsm = new FSM(enemyHeadBlackborad);
        fsm.AddState(StateType.MOVEDOWN,new AI_MoveDown(fsm));
        fsm.AddState(StateType.MOVEUP,new AI_MoveUp(fsm));

        fsm.SwitchState(StateType.MOVEDOWN);
        
    }

    private void Update() {
        fsm.OnUpdate();
    }

    private void FixedUpdate() {
        fsm.OnFixedUpdate();
    }
}
