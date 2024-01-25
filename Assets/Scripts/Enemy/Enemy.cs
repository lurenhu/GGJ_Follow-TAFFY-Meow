using UnityEngine;


[System.Serializable]
public class EnemyBlackborad : Blackborad
{
    public Transform attackPosition;
    public Transform defancePosition;
    public Transform enemyFist;

    public float attackTime;
    public float defanceTime;
    public float randomRadius;
    [Range(50,200)] public float traceWeight;

}

public class AI_Attack : IState
{
    
    float attackTimer;//进攻冷却时间
    Vector2 targetPosition;
    Rigidbody2D fistRB2D;

    //基本数据
    FSM fsm;
    EnemyBlackborad enemyBlackborad;


    public AI_Attack(FSM fsm)
    {
        this.fsm = fsm;
        this.enemyBlackborad = fsm.blackborad as EnemyBlackborad;
    }
    public void OnCheck()
    {
    }

    public void OnEnter()
    {
        attackTimer = 0;
        
        float RandomX = Random.Range(-enemyBlackborad.randomRadius,enemyBlackborad.randomRadius);
        float RandomY = Random.Range(-enemyBlackborad.randomRadius,enemyBlackborad.randomRadius);
        targetPosition = new Vector2(
            enemyBlackborad.attackPosition.position.x + RandomX,
            enemyBlackborad.attackPosition.position.y + RandomY
            );

        fistRB2D = enemyBlackborad.enemyFist.GetComponent<Rigidbody2D>();
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
        Vector2 Direction = (targetPosition - (Vector2)enemyBlackborad.enemyFist.position).normalized;
        fistRB2D.AddForce(Direction * enemyBlackborad.traceWeight);
    }

    public void OnUpdate()
    {
        
        attackTimer += Time.deltaTime;
        if (attackTimer >= enemyBlackborad.attackTime)
        {
            fsm.SwitchState(StateType.DEFANCE);
        }
    }
}

public class AI_Defance : IState
{
    float defanceTimer;
    Vector2 targetPosition;
    Rigidbody2D fistRB2D;

    FSM fsm;
    EnemyBlackborad enemyBlackborad;
    public AI_Defance(FSM fsm)
    {
        this.fsm = fsm;
        this.enemyBlackborad = fsm.blackborad as EnemyBlackborad;
    }
    public void OnCheck()
    {
    }

    public void OnEnter()
    {
        defanceTimer = 0;

        float RandomX = Random.Range(-enemyBlackborad.randomRadius,enemyBlackborad.randomRadius);
        float RandomY = Random.Range(-enemyBlackborad.randomRadius,enemyBlackborad.randomRadius);
        targetPosition = new Vector2(
            enemyBlackborad.defancePosition.position.x + RandomX,
            enemyBlackborad.defancePosition.position.y + RandomY
            );

        fistRB2D = enemyBlackborad.enemyFist.GetComponent<Rigidbody2D>();
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
        Vector2 Direction = (targetPosition - (Vector2)enemyBlackborad.enemyFist.position).normalized;
        fistRB2D.AddForce(Direction * enemyBlackborad.traceWeight);
    }

    public void OnUpdate()
    {
        defanceTimer += Time.deltaTime;
        if (defanceTimer >= enemyBlackborad.defanceTime)
        {
            fsm.SwitchState(StateType.ATTACK);
        }
    }
}


public class Enemy : MonoBehaviour
{
    private FSM fsm;
    public EnemyBlackborad enemyBlackborad;

    private void Start() {
        fsm = new FSM(enemyBlackborad);
        fsm.AddState(StateType.ATTACK,new AI_Attack(fsm));
        fsm.AddState(StateType.DEFANCE,new AI_Defance(fsm));

        fsm.SwitchState(StateType.ATTACK);
        
    }

    private void Update() {
        fsm.OnUpdate();
    }

    private void FixedUpdate() {
        fsm.OnFixedUpdate();
    }
}
