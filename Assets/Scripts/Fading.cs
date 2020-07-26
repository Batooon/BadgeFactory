using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _fadeOutDelay;

    public void FadeOut()
    {
        _canvasGroup.LeanAlpha(0, _fadeDuration).setDelay(_fadeOutDelay);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void FadeIn()
    {
        _canvasGroup.LeanAlpha(1, _fadeDuration);
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void FadeInOut()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.LeanAlpha(1, _fadeDuration).setOnComplete(FadeOut);
    }

}
