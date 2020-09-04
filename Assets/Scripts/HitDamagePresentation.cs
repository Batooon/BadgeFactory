using Automations;
using TMPro;
using UnityEngine;

public class HitDamagePresentation : MonoBehaviour
{
    [SerializeField] private Gradient _textColorsBasedOnDamageAmount;
    [SerializeField] private string _textTemplate;
    [SerializeField] private TextMeshProUGUI _hitText;
    [SerializeField] private float _movingTime;

    private AutomationsData _automationsData;

    public void Init(AutomationsData automationsData)
    {
        _automationsData = automationsData;
    }

    public void SpawnHit(long currentHitValue)
    {
        float value = Mathf.InverseLerp(_automationsData.ClickPower,
            _automationsData.ClickPower + _automationsData.ClickPower * _automationsData.CriticalPowerIncreasePercentage,
            currentHitValue);
        _hitText.color = _textColorsBasedOnDamageAmount.Evaluate(value);
        _hitText.text = string.Format(_textTemplate, currentHitValue.ConvertValue());
        gameObject.SetActive(true);
        LeanTween.move(gameObject, new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)), _movingTime).setOnComplete(() => gameObject.SetActive(false));
    }
}
