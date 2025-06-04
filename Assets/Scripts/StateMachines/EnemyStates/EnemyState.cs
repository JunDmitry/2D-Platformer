using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : State<EnemyStateType>
{
    protected EnemyState(StateMachine<EnemyStateType> stateMachine, EnemyStateType id) : base(stateMachine, id)
    {
    }
}