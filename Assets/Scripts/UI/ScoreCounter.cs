using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private int _scoreCount = 0;
    private Text _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<Text>();
    }

    public void UpdateScore()
    {
        _scoreCount += 1;
    }

    public void UpdateScoreText()
    {
        _scoreText.text = _scoreCount.ToString();
    }
}
