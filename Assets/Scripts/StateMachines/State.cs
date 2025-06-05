using System;
using System.Collections.Generic;

public abstract class State
{
    protected readonly IStateChanger StateMachine;
    protected readonly List<Transition> TransitionsUpdate;
    protected readonly List<Transition> TransitionsFixedUpdate;

    protected State(IStateChanger stateMachine)
    {
        StateMachine = stateMachine;
        TransitionsUpdate = new();
        TransitionsFixedUpdate = new();
    }

    public void AddUpdateTransition(Transition transition)
    {
        ValidateTransition(TransitionsUpdate, transition);
        TransitionsUpdate.Add(transition);
    }

    public void AddFixedUpdateTransition(Transition transition)
    {
        ValidateTransition(TransitionsFixedUpdate, transition);
        TransitionsFixedUpdate.Add(transition);
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public void Update()
    {
        OnUpdate();

        if (TryTransit(TransitionsUpdate))
            return;

        OnUpdating();
    }

    public void FixedUpdate()
    {
        OnFixedUpdate();

        if (TryTransit(TransitionsFixedUpdate))
            return;

        OnFixedUpdating();
    }

    protected virtual void OnUpdate()
    {
    }

    protected virtual void OnUpdating()
    {
    }

    protected virtual void OnFixedUpdate()
    {
    }

    protected virtual void OnFixedUpdating()
    {
    }

    private void ValidateTransition(List<Transition> transitions, Transition transition)
    {
        if (transition == null)
            throw new ArgumentNullException(nameof(transition), "Transition cannot be null!");

        if (transitions.Contains(transition))
            throw new ArgumentException($"There is already such a transition in this {nameof(State)}.", nameof(transition));
    }

    private bool TryTransit(IEnumerable<Transition> transitions)
    {
        foreach (Transition transition in transitions)
        {
            if (transition.TryTransit(out State nextState) == false)
                continue;

            StateMachine.ChangeState(nextState);
            return true;
        }

        return false;
    }
}