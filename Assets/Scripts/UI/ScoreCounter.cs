using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private FloatReference _scoreCount;
    [SerializeField]
    private FloatReference _upgradeCost;

    private Text _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<Text>();
        _scoreText.text = $"Score: {_scoreCount.Value}";
    }

    public void UpgradePurchased()
    {
        _scoreCount.Variable.ApplyChange(-_upgradeCost);
        _scoreText.text = $"Score: {_scoreCount.Value}";
    }

    public void UpdateScoreText(FloatVariable _scoreAmount)
    {
        _scoreCount.Variable.ApplyChange(_scoreAmount);
        _scoreText.text = $"Score: {_scoreCount.Value}";
    }
}
