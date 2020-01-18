using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTrackingWrapper<T>
{
    public Action<T> OnValueChange;

    private T _value;
    public bool HasChanged { get; private set; }

    public ChangeTrackingWrapper()
    {

    }

    public ChangeTrackingWrapper(T initialValue, Action<T> onValueChange)
    {
        _value = initialValue;
        OnValueChange = onValueChange;
    }

    public T Value
    {
        get { return _value; }
        set
        {
            if (_value.Equals(value)) { return; }
            _value = value;
            OnValueChange?.Invoke(_value);
            HasChanged = true;
        }
    }
    
    public void ResetChangedFlag()
    {
        HasChanged = false;
    }

}
