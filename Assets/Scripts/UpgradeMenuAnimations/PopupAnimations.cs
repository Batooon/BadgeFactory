using UnityEngine;
using UnityEngine.Events;

//Developer: Antoshka

public class PopupAnimations : MonoBehaviour
{ 
    public float StartingPositionY;
    public float DestinationPositionY;
    public float Time;
    public float Delay;

    public UnityEvent OnActivated;
    public UnityEvent OnDeactivated;

    public void Activate()
    {
        OnActivated?.Invoke();
        LeanTween.moveLocalY(gameObject, DestinationPositionY, Time).setDelay(Delay);
    }

    public void Deactivate()
    {
        LeanTween.moveLocalY(gameObject, StartingPositionY, Time).setDelay(Delay).setOnComplete(OnDeactivated.Invoke);
    }
}
