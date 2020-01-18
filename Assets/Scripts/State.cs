using UnityEngine;
using System;

public abstract class State : ITick
{
    protected float _timeBeforeTransition;  //automatically switches state after this time has passed
    protected Type _nextState;
    protected Transform _transform;

    public State(float timeBeforeTransition, Type nextState, Transform transform)
    {
        _timeBeforeTransition = timeBeforeTransition;
        _nextState = nextState;
        _transform = transform;
    }

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public virtual Type Tick(float deltaTime)
    {
        _timeBeforeTransition -= deltaTime;

        if (_timeBeforeTransition <= 0)
        {
            return _nextState;
        }

        return null;
    }
}

public interface ITick
{
    Type Tick(float deltaTime);
}
