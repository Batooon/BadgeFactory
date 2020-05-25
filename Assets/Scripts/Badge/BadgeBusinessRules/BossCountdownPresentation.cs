﻿using TMPro;
using UnityEngine;

namespace Badge.BusinessRules
{
    public class BossCountdownPresentation : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _countdownText;

        private void Start()
        {
            HideTimer();
        }

        public void UpdateCountdown(int value)
        {
            _countdownText.text = value.ToString();
        }

        public void ShowTimer()
        {
            _countdownText.gameObject.SetActive(true);
        }

        public void HideTimer()
        {
            _countdownText.gameObject.SetActive(false);
        }
    }
}