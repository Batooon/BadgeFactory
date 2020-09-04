using System;
using System.Collections;
using UnityEngine;

namespace Badge
{
    [RequireComponent(typeof(BossCountdownPresentation))]
    public class BossCountdown : MonoBehaviour, IBossCountdown
    {
        public event Action BossNotDefeated;
        private Coroutine _countdown;
        private BossCountdownPresentation _bossCountdownPresentation;

        public void Init()
        {
            _bossCountdownPresentation = GetComponent<BossCountdownPresentation>();
        }

        public void StartCountdown(int timer)
        {
            timer = 30;
            _bossCountdownPresentation.ShowTimer();
            _countdown = StartCoroutine(Countdown(timer));
        }

        public void StopCountdown()
        {
            if(_countdown!=null)
                StopCoroutine(_countdown);
            _bossCountdownPresentation.HideTimer();
        }

        private IEnumerator Countdown(int countdownTime)
        {
            int time = countdownTime;
            while (time > 0)
            {
                time -= 1;
                _bossCountdownPresentation.UpdateCountdown(time);
                yield return new WaitForSeconds(1f);
            }
            BossNotDefeated?.Invoke();
            _bossCountdownPresentation.HideTimer();
            yield break;
        }
    }
}