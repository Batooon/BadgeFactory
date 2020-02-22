using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Developer: Antoshka

public class ClickTracker : MonoBehaviour
{
    public Camera UICamera;
    public GameObject HitEffect;

    public void Clicked()
    {

#if UNITY_EDITOR
        Vector3 effectPosition = UICamera.ScreenToWorldPoint(Input.mousePosition);
        effectPosition.z = 80f;
        GameObject effect = Instantiate(HitEffect, effectPosition, Quaternion.identity);
        Destroy(effect, 0.7f);
#endif

#if UNITY_ANDROID
        foreach (Touch touch in Input.touches)
        {
            Ray ray = UICamera.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray))
            {
                Instantiate(HitEffect, touch.position, Quaternion.identity, null);
                Destroy(HitEffect, 3f);
            }
        }
#endif
    }
}
