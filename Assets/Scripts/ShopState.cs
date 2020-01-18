using System;
using UnityEngine;

public class ShopState : State
{
    private CardShop _cardShop;
    
    public ShopState(float timeBeforeTransition, Type nextState, Transform transform) : base(timeBeforeTransition, nextState, transform)
    {
        _cardShop = transform.GetComponent<CardShop>();
    }

    public override void OnStateEnter()
    {
        //place player cards on the board
        
        //refresh shop
        //show shop cards
        //show shop buttons
        _cardShop.RefreshShop();
    }

    public override Type Tick(float deltaTime)
    {
        return base.Tick(deltaTime);
    }

    public override void OnStateExit()
    {
        //hide shop cards
        //hide shop buttons
        //disable heropower button from being pressed
    }
}
