using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Developer: Antoshka

public class PopupAnimations : MonoBehaviour
{
    public AnimationCurve AnimationCurve; 
    public float StartingPositionY;
    public float DestinationPositionY;
    public float Time;
    public float Delay;

    public void Activate()
    {
        LeanTween.moveLocalY(gameObject, DestinationPositionY, Time).setDelay(Delay);
    }

    public void Deactivate()
    {
        LeanTween.moveLocalY(gameObject, StartingPositionY, Time).setDelay(Delay).setOnComplete(() => gameObject.SetActive(false));
    }
}
