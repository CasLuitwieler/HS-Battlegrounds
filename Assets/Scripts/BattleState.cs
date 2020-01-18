using System;
using UnityEngine;

public class BattleState : State
{
    public BattleState(float timeBeforeTransition, Type nextState, Transform transform) : base(timeBeforeTransition, nextState, transform) { }

    public override void OnStateEnter()
    {
        //get enemy cards and place them on the board
    }

    public override Type Tick(float deltaTime)
    {
        //play battle
        return base.Tick(deltaTime);
    }

    public override void OnStateExit()
    {
        //remove all cards from the board
        //reset stats to default?
    }
}
