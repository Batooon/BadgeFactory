using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

/// <summary>
/// Contains All display actions
/// Recieves Input
/// Talks to Controller to execute logic
/// Assumes callbacks added by Controller
/// Blind to Model and Controller existance
/// </summary>

public class BadgeView : MonoBehaviour
{
    #region Callbacks
    public BadgeInputCallback InputCallback;
    public CoinCreatedCallback CoinCallback;
    #endregion
    #region UI
    [SerializeField]
    private Image _badgeImage;
    [SerializeField]
    private Image _hpFiller;

    private Button _badgeButton;
    [SerializeField]
    private GameObject _particleEffect;
    [SerializeField]
    private Camera UICamera;
    [SerializeField]
    private GameObject CoinPrefab;

    [SerializeField]
    private TextMeshProUGUI _bossCountdownText;
    #endregion

    public void BadgeClicked()
    {
        InputCallback?.Invoke();
    }

    public void IncreaseBadgeImageAlpha(float max, float current)
    {
        var tempColor = _badgeImage.color;
        tempColor.a = Mathf.Clamp01(Mathf.InverseLerp(.2f, max, current));
        _badgeImage.color = tempColor;
    }

    public void SpawnCoins(int amount, int coinCost)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject coin = Instantiate(CoinPrefab, transform.position, Quaternion.identity);
            coin.transform.SetParent(transform.parent, false);
            CoinCallback?.Invoke(coin, coinCost, 5f);
        }
    }

    public void SetupBossUI(float countdownTime)
    {
        _bossCountdownText.text = countdownTime.ToString();
        _bossCountdownText.gameObject.SetActive(true);
    }

    public void SetupBadge(Badge currentBadge)
    {
        _badgeImage.sprite = currentBadge.BadgeSprite;

        var tempColor = _badgeImage.color;
        tempColor.a = 0;
        _badgeImage.color = tempColor;
    }

    public void UpdateHp(float maxValue, float currentValue)
    {
        _hpFiller.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(0f, maxValue, currentValue));
    }

    private IEnumerator StartCountdown(float countdown)
    {
        while (countdown > 0)
        {
            countdown--;
            _bossCountdownText.text = countdown.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        _bossCountdownText.gameObject.SetActive(false);
    }

    private void Awake()
    {
        _badgeButton = GetComponent<Button>();
        _badgeButton.onClick.AddListener(BadgeClicked);
    }

    private void Update()
    {
#if UNITY_ANDROID
        Touch[] touches = Input.touches;
        if (touches.Length > 0)
        {
            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i].phase == TouchPhase.Began)
                {
                    Vector3 effectPosition = UICamera.ScreenToWorldPoint(touches[i].position);
                    RaycastHit2D hit = Physics2D.Raycast(effectPosition, Vector2.zero);
                    effectPosition.z = 80f;
                    GameObject effect = Instantiate(_particleEffect, effectPosition, Quaternion.identity);
                    Destroy(effect, .7f);
                }
            }
        }
#endif
    }

}
