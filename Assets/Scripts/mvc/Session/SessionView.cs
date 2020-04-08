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

    public void FetchAllComponents(Data playerData)
    {
        FetchGold(playerData.GoldAmount);
        FetchLevel(playerData.Level);
        FetchLevel(playerData.LevelProgress);
    }

    public void ActivateReturningPlayerWindow(int gainedGoldAmount)
    {
        _returnedAmountOfGold.text = gainedGoldAmount.ToString();
        _returningPlayerWindow.SetActive(true);
    }

    public void FetchGold(int goldAmount)
    {
        _goldText.text = goldAmount.ConvertValue();
    }

    public void FetchLevel(int level)
    {
        _levelText.text = $"Level {level}";
    }

    public void FetchLevelProgress(int levelProgress)
    {
        _levelFiller.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(0, 10, levelProgress));
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
