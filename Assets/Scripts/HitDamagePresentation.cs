using Automations;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class HitDamagePresentation : MonoBehaviour
{
    [SerializeField] private Gradient _textColorsBasedOnDamageAmount;
    [SerializeField] private string _textTemplate;
    [SerializeField] private TextMeshProUGUI _hitText;
    [SerializeField] private float _movingTime;
    [Range(2f, 5f)] [SerializeField] private float _movingOffset;

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
        float x = Random.Range(transform.position.x - _movingOffset, transform.position.x + _movingOffset);
        float y = Random.Range(transform.position.y - _movingOffset, transform.position.y + _movingOffset);
        transform.DOMove(new Vector3(x, y, transform.position.z), _movingTime).OnComplete(() => gameObject.SetActive(false));
    }
}
