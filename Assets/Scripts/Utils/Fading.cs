using UnityEngine;
using UnityEngine.Events;

public class Fading : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _fadeOutDelay;
    [SerializeField] private UnityEvent _fadingCompleted;

    public void FadeOut()
    {
        _canvasGroup.LeanAlpha(0, _fadeDuration).setDelay(_fadeOutDelay).setOnComplete(_fadingCompleted.Invoke);
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
