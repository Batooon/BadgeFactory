using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Default Automations Data")]
public class DefaultAutomationsData : ScriptableObject
{
    [SerializeField, HideInInspector] private List<Automation> _automations;
    [SerializeField] private Automation _automation;

    private int _automationIndex = 0;

    public int AutomationIndex => _automationIndex;

    public List<Automation> Automations => _automations;

    public void AddAutomation()
    {
        if (_automations == null)
            _automations = new List<Automation>();

        _automation = new Automation();
        _automations.Add(_automation);
        _automationIndex = _automations.Count - 1;
    }

    public void RemoveAutomation()
    {
        if (_automationIndex > 0)
        {
            _automation = _automations[--_automationIndex];
            _automations.RemoveAt(++_automationIndex);
        }
        else
        {
            _automations.Clear();
            _automation = null;
        }
    }

    public Automation GetNext()
    {
        if (_automationIndex < _automations.Count - 1)
            _automationIndex++;

        _automation = this[_automationIndex];
        return _automation;
    }

    public Automation GetPrevious()
    {
        if (_automationIndex > 0)
            _automationIndex--;

        _automation = this[_automationIndex];
        return _automation;
    }

    public void ClearAutomations()
    {
        _automations.Clear();
        _automations.Add(new Automation());
        _automation = _automations[0];
        _automationIndex = 0;
    }

    public Automation this[int index]
    {
        get
        {
            if (_automations != null && _automationIndex >= 0 && _automationIndex < _automations.Count)
                return _automations[index];
            return null;
        }
        set
        {
            if (_automations == null)
                _automations = new List<Automation>();

            if (_automationIndex >= 0 && _automationIndex < _automations.Count && value != null)
                _automations[index] = value;
            else
                Debug.LogError("Выход за границы массива или переданное значение null");
        }
    }
}
