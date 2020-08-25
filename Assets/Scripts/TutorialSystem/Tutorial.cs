using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tutorialSteps;
    [SerializeField] private long _requiredGoldAmountToUnlockSecondStep;
    [SerializeField] private UnityEvent _firstStep;
    [SerializeField] private UnityEvent _secondStep;
    [SerializeField] private UnityEvent _thirdStep;

    private PlayerData _playerData;
    private int _currentTutorialStep;

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
    }

    private void UpdateSteps()
    {
        for (int i = 0; i < _tutorialSteps.Count; i++)
            _tutorialSteps[i].SetActive(_currentTutorialStep == i);
    }

    private void Update()
    {
        if (_playerData.IsReturningPlayer)
            return;
        if (_currentTutorialStep > _tutorialSteps.Count - 1)
            return;

        if (_currentTutorialStep == 0)
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
                        ActionCompleted();
                    }
                }
            }
        }
        else if (_currentTutorialStep == 1)
        {
            if (_playerData.Gold >= _requiredGoldAmountToUnlockSecondStep)
            {
                UpdateSteps();
                _secondStep?.Invoke();
            }
        }
        else if (_currentTutorialStep == 2)
        {
            if (_playerData.Level >= 75)
            {
                UpdateSteps();
                _thirdStep?.Invoke();
                _playerData.IsReturningPlayer = true;
            }
        }
    }

    public void ActionCompleted()
    {
        if (_currentTutorialStep > _tutorialSteps.Count - 1)
            return;
        _tutorialSteps[_currentTutorialStep].SetActive(false);
        _currentTutorialStep += 1;
    }


    /*
    [SerializeField] private TutorialStepData[] _tutorialStepsData;
    [SerializeField] private TutorialStepPresentation _tutorialStepBlank;
    [SerializeField] private List<GameObject> _tutorialSteps;
    [SerializeField] private long _goldAmountRequiredToUnlockAutomations;
    [SerializeField] private UnityEvent _secondTutorialStep;

    private PlayerData _playerData;
    private event Action<int> _tutorialStepChanged;
    //private Dictionary<TutorialStepPresentation, TutorialStepData> _tutorialSteps;
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

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
        /*foreach (var item in _tutorialStepsData)
        {
            TutorialStepPresentation tutorialStepPresentation = Instantiate(_tutorialStepBlank.gameObject).GetComponent<TutorialStepPresentation>();
            _tutorialSteps.Add(tutorialStepPresentation, item);
        }

        foreach (var item in _tutorialSteps)
            item.Key.Init(item.Value, _tutorialStepChanged);
}

    private void OnEnable()
    {
        _playerData.GoldChanged += OnGoldAmountChanged;
    }

    private void OnDisable()
    {
        _playerData.GoldChanged -= OnGoldAmountChanged;
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
        else if (_tutorialStep == 2)
        {
            _secondTutorialStep?.Invoke();
            _tutorialStep += 1;
        }
    }

    private void OnGoldAmountChanged(long newAmount)
    {
        if (newAmount >= _goldAmountRequiredToUnlockAutomations)
            _tutorialStep += 1;
    }*/
}
