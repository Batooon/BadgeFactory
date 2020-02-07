using UnityEngine;

//Developer: Antoshka
[CreateAssetMenu]
public class IntVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    [SerializeField]
    private string Developerdescription = "";
#endif

    public int Value;

    public void SetValue(int _value)
    {
        Value = _value;
    }

    public void SetValue(IntVariable _value)
    {
        Value = _value.Value;
    }

    public void ApplyChange(int _amount)
    {
        Value += _amount;
    }

    public void ApplyChange(IntVariable _amount)
    {
        Value += _amount.Value;
    }
}
