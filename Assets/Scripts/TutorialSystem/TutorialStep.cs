using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TutorialStepPresentation))]
public class TutorialStep : MonoBehaviour
{
    private TutorialStepPresentation _tutorialStepPresentation;

    public void Init(TutorialStepData tutorialStepData)
    {
        _tutorialStepPresentation = GetComponent<TutorialStepPresentation>();
    }
}
