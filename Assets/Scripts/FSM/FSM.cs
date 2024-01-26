using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    //此处放置所需要的状态枚举名称
    ATTACK,
    DEFANCE,
    MOVEDOWN,
    MOVEUP
}

[Serializable]
public class Blackborad
{
    //此处存储共享数据，或者向外展示的数据，可配置数据
}

public class FSM
{
    public IState curState;
    public Dictionary<StateType,IState> states;
    public Blackborad blackborad;

    public FSM(Blackborad blackborad) 
    {
        this.states = new Dictionary<StateType, IState>();
        this.blackborad = blackborad;
    }

    /// <summary>
    /// 添加状态及状态的阶段至字典
    /// </summary>
    public void AddState(StateType stateType, IState state)
    {
        if (states.ContainsKey(stateType))
        {
            Debug.Log("[AddState] >>>>> map has contain key " + stateType);
            return;
        }
        states.Add(stateType,state);
    }

    /// <summary>
    /// 切换状态至stateType
    /// </summary>
    public void SwitchState(StateType stateType)
    {
        if (!states.ContainsKey(stateType))
        {
            Debug.Log("[SwitchState] >>>>> map has not contain key " + stateType);
            return;
        }
        if (curState != null)
        {
            curState.OnExit();
        }
        curState = states[stateType];
        curState.OnEnter();
    }

    public void OnUpdate()
    {
        curState.OnUpdate();
        curState.OnCheck();
    }

    public void OnFixedUpdate()
    {
        curState.OnFixedUpdate();
    }
}

public interface IState 
{
    void OnEnter();
    void OnUpdate();
    void OnExit();
    void OnCheck();
    void OnFixedUpdate();
}
