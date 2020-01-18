using System;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public Action OnCombatStart, OnCombatEnd, OnShopStart, OnShopEnd;

    [SerializeField]
    private float _initialDelay = 1f, _shopDuration = 10f, _battleDuration = 10f;
    
    private State _currentState;
    private Type _shopState, _battleState;
    private Dictionary<Type, State> _states = new Dictionary<Type, State>();

    private void Awake()
    {
        _shopState = typeof(ShopState);
        _battleState = typeof(BattleState);

        _states.Add(_shopState, new ShopState(_shopDuration, _battleState, transform));
        _states.Add(_battleState, new BattleState(_battleDuration, _shopState, transform));

        //TODO: after initial delay set currentState
        _currentState = _states[_shopState];
    }

    private void Update()
    {
        Type nextState = _currentState.Tick(Time.deltaTime);

        HandleStateTransition(nextState);
    }

    private void HandleStateTransition(Type nextState)
    {
        if (nextState == null) { return; }

        if (_currentState != null)
        {
            _currentState.OnStateExit();
        }

        _currentState = _states[nextState];

        _currentState.OnStateEnter();
    }
}
