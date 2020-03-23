using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SessionView : MonoBehaviour
{
    #region Callbacks
    public QuitGameCallback QuitCallback;
    #endregion
    #region UI
    [SerializeField]
    private GameObject _exitWindow;
    [SerializeField]
    private Button _exitButton;
    [SerializeField]
    private Button _closeExitMenuButton;

    [SerializeField]
    private GameObject _returningPlayerWindow;
    [SerializeField]
    private TextMeshProUGUI _returnedAmountOfGold;

    [SerializeField]
    private TextMeshProUGUI _levelText;
    [SerializeField]
    private TextMeshProUGUI _goldText;

    [SerializeField]
    private Image _levelFiller;
    #endregion

    public void ActivateReturningPlayerWindow(int gainedGoldAmount)
    {
        _returnedAmountOfGold.text = gainedGoldAmount.ToString();
        _returningPlayerWindow.SetActive(true);
    }

    public void FetchUI(Data playerData)
    {
        _levelText.text = $"Level {playerData.Level}";
        _goldText.text = playerData.GoldAmount.ConvertValue();
        _levelFiller.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(0, 10, playerData.levelProgress));
    }

    public void LevelUp(int levelProgress)
    {
        _levelFiller.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(0, 10, levelProgress));
    }

    private void Awake()
    {/*
        _closeExitMenuButton.onClick.AddListener(() => _exitWindow.SetActive(false));
        _exitButton.onClick.AddListener(ExitGame);*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _exitWindow.SetActive(true);
        }
    }

    private void ExitGame()
    {
        QuitCallback?.Invoke();
    }
}
