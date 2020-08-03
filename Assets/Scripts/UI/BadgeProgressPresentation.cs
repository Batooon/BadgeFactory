using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeProgressPresentation : MonoBehaviour
{
    [SerializeField] private Slider _progressSlider;

    private BadgeData _badgeData;

    public void Init(BadgeData badgeData)
    {
        _badgeData = badgeData;

        _badgeData.HpChanged += UpdateSlider;
        _badgeData.MaxHpChanged += UpdateMaxSliderValue;
    }

    private void OnEnable()
    {
        _badgeData.HpChanged += UpdateSlider;
        _badgeData.MaxHpChanged += UpdateMaxSliderValue;

        UpdateSlider(_badgeData.CurrentHp);
        UpdateMaxSliderValue(_badgeData.MaxHp);
    }

    private void OnDisable()
    {
        _badgeData.HpChanged -= UpdateSlider;
        _badgeData.MaxHpChanged -= UpdateMaxSliderValue;
    }

    private void Start()
    {
        UpdateSlider(_badgeData.CurrentHp);
        UpdateMaxSliderValue(_badgeData.MaxHp);
    }

    private void UpdateSlider(long badgeHp)
    {
        _progressSlider.value = badgeHp;
    }

    private void UpdateMaxSliderValue(long maxValue)
    {
        _progressSlider.maxValue = maxValue;
    }
}
