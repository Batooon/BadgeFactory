using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BoxRewardPresentation : MonoBehaviour
{
    [SerializeField] private string _rewardTemplate;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private UnityEvent _activatePanel;

    public void ActivatePanel(long rewardValue)
    {
        _rewardText.text = string.Format(_rewardTemplate, rewardValue.ConvertValue());
        _activatePanel?.Invoke();
    }
}
