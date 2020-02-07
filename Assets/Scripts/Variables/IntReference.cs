using System;
using UnityEngine;

//Developer: Antoshka
[Serializable]
public class IntReference
{
    public bool UseConstant = true;
    public int ConstantValue;
    public IntVariable Variable;

    public IntReference()
    {

    }

    public IntReference(int _value)
    {
        UseConstant = true;
        Variable.Value = _value;
    }

    public int Value
    {
        get
        {
            return UseConstant ? ConstantValue : Variable.Value;
        }
    }

    public static implicit operator int(IntReference reference)
    {
        return reference.Value;
    }
}
