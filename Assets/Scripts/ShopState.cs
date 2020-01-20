using System;
using UnityEngine;

public class ShopState : State
{
    private ShopCards _shopCards;
    
    public ShopState(float timeBeforeTransition, Type nextState, Transform transform) : base(timeBeforeTransition, nextState, transform)
    {
        _shopCards = transform.GetComponent<ShopCards>();
    }

    public override void OnStateEnter()
    {
        //place player cards on the board
        
        //refresh shop
        //show shop cards
        //show shop buttons
        _shopCards.RefreshShop();
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
