using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TutorialStepData
{
    [SerializeField] private string _tutorialText;
    [SerializeField] private Sprite _tutorialSprite;
    [SerializeField] private int _tutorialStepIndex;

    public string TutorialText => _tutorialText;
    public Sprite TutorialSprite => _tutorialSprite;
    public int TutorialStepIndex => _tutorialStepIndex;
}
