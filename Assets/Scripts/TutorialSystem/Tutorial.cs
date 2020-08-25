using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialStepData[] _tutorialStepsData;
    [SerializeField] private TutorialStepPresentation _tutorialStepBlank;

    private event Action<int> _tutorialStepChanged;
    private Dictionary<TutorialStepPresentation, TutorialStepData> _tutorialSteps;
    private int _currentTutorialStep;
    private int _tutorialStep
    {
        get => _currentTutorialStep;
        set
        {
            _currentTutorialStep = value;
            _tutorialStepChanged?.Invoke(value);
        }
    }

    public void Init()
    {
        foreach (var item in _tutorialStepsData)
        {
            TutorialStepPresentation tutorialStepPresentation = Instantiate(_tutorialStepBlank.gameObject).GetComponent<TutorialStepPresentation>();
            _tutorialSteps.Add(tutorialStepPresentation, item);
        }

        foreach (var item in _tutorialSteps)
            item.Key.Init(item.Value, _tutorialStepChanged);
    }

    public void StartTutorial()
    {
        _tutorialStep = 0;
    }

    private void Update()
    {
        if (_tutorialStep == 0)
        {
            Touch[] touches = Input.touches;

            if (touches.Length > 0)
            {
                for (int i = 0; i < touches.Length; i++)
                {
                    if (touches[i].phase == TouchPhase.Began)
                    {
                        if (EventSystem.current.IsPointerOverGameObject(touches[i].fingerId))
                            return;
                        _tutorialStep += 1;
                    }
                }
            }
        }
        else if (_tutorialStep == 1)
        {

        }
    }
}
