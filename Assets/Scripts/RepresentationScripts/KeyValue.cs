using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE
{
    ZERO,
    SINGLE,
    DOUBLE,
    TRIPLE,
    WIN
}

public class KeyValue
{
    public STATE State;
    public Vector2Int Action;//coords of the tile being placed
    public float QValue;//qValue of the current state, action
    
    public KeyValue(STATE state, Vector2Int action, float qValue)
    {
        State = state;
        Action = action;
        QValue = qValue;
        Debug.Log($"new key value pair: State({State}) Action({Action}) QValue({QValue})");
    }
}
