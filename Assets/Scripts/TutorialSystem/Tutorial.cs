using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private UnityEvent _progressResetterUnlocked;
    [SerializeField] private long _levelToUnlcokProgressReset;

    private PlayerData _playerData;

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
    }

    private void OnEnable()
    {
        _playerData.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _playerData.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int newLevel)
    {
        if (newLevel == _levelToUnlcokProgressReset)
        {
            _progressResetterUnlocked?.Invoke();
        }
    }
    /*
    [SerializeField] public List<GameObject> TutorialSteps;
    [SerializeField] public long RequiredGoldAmountToUnlockSecondStep;
    [SerializeField] public UnityEvent FirstStep;
    [SerializeField] public UnityEvent SecondStep;
    [SerializeField] public UnityEvent ThirdStep;
    [SerializeField, HideInInspector] public PlayerData PlayerData;

    public void Init(PlayerData playerData)
    {
        PlayerData = playerData;
    }

    private void Start()
    {
        StartCoroutine(_tutorialState.Start());
    }

    public void SecondStepCompleted()
    {
        foreach (var item in TutorialSteps)
            item.SetActive(false);
        SetState(new ThirdStep(this));
    }

    public void ThirdStepCompleted()
    {
        foreach (var item in TutorialSteps)
            item.SetActive(false);
        SetState(new TutorialEndedStep(this));
    }*/
}
/*
public abstract class TutorialState
{
    protected int _tutorialStepIndex;
    protected Tutorial _tutorial;

    public TutorialState(Tutorial tutorial)
    {
        _tutorial = tutorial;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }
}

public class FirstStep : TutorialState
{
    public FirstStep(Tutorial tutorial) : base(tutorial)
    {
        _tutorialStepIndex = 0;
    }

    public override IEnumerator Start()
    {
        _tutorial.TutorialSteps[_tutorialStepIndex].SetActive(true);
        Touch[] touches = Input.touches;

        if (touches.Length > 0)
        {
            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i].phase == TouchPhase.Began)
                {
                    if (EventSystem.current.IsPointerOverGameObject(touches[i].fingerId))
                        yield return null;
                    _tutorial.TutorialSteps[_tutorialStepIndex].SetActive(false);
                    _tutorial.SetState(new SecondStep(_tutorial));
                }
            }
        }
        yield return null;
    }
}

public class SecondStep : TutorialState
{
    public SecondStep(Tutorial tutorial) : base(tutorial)
    {
        _tutorialStepIndex = 1;
    }

    public override IEnumerator Start()
    {
        if (_tutorial.PlayerData.Gold >= _tutorial.RequiredGoldAmountToUnlockSecondStep)
        {
            _tutorial.TutorialSteps[_tutorialStepIndex].SetActive(true);
            _tutorial.SecondStep?.Invoke();
        }

        yield return null;
    }
}

public class ThirdStep : TutorialState
{
    public ThirdStep(Tutorial tutorial) : base(tutorial)
    {
        _tutorialStepIndex = 2;
    }

    public override IEnumerator Start()
    {
        if (_tutorial.PlayerData.Level >= 75)
        {
            _tutorial.TutorialSteps[_tutorialStepIndex].SetActive(true);
            _tutorial.ThirdStep?.Invoke();
        }
        yield return null;
    }
}

public class TutorialEndedStep : TutorialState
{
    public TutorialEndedStep(Tutorial tutorial) : base(tutorial)
    {
    }
}

public abstract class StateMachine : MonoBehaviour
{
    protected TutorialState _tutorialState;

    public void SetState(TutorialState tutorialState)
    {
        _tutorialState = tutorialState;
        StartCoroutine(_tutorialState.Start());
    }
}*/
