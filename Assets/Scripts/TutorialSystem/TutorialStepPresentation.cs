using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStepPresentation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tutorialText;
    [SerializeField] private Image _tutorialImage;

    private TutorialStepData _tutorialStepData;
    private Action<int> _tutorialIndexChanged;

    public void Init(TutorialStepData tutorialStepData, Action<int> tutorialIndexChanged)
    {
        _tutorialStepData = tutorialStepData;
        _tutorialIndexChanged = tutorialIndexChanged;
        _tutorialIndexChanged += OnTutorialIndexChanged;
    }

    private void OnEnable()
    {
        _tutorialText.text = _tutorialStepData.TutorialText;
        _tutorialImage.sprite = _tutorialStepData.TutorialSprite;
    }
    
    private void OnTutorialIndexChanged(int newIndex)
    {
        gameObject.SetActive(_tutorialStepData.TutorialStepIndex == newIndex);
    }

    private void OnDestroy()
    {
        _tutorialIndexChanged -= OnTutorialIndexChanged;
    }
}
