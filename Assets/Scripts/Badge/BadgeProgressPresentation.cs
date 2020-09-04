using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BadgeProgressPresentation : MonoBehaviour
{
    [SerializeField] private string _badgeProgressTemplate = "{0}/{1}";
    [SerializeField] private TextMeshProUGUI _progressText;
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

    private void UpdateSlider(float badgeHp)
    {
        _progressSlider.value = badgeHp;
        _progressText.text = string.Format(_badgeProgressTemplate, Mathf.RoundToInt(badgeHp), _badgeData.MaxHp);
    }

    private void UpdateMaxSliderValue(long maxValue)
    {
        _progressSlider.maxValue = maxValue;
        _progressText.text = string.Format(_badgeProgressTemplate, _badgeData.CurrentHp, maxValue);
    }
}
