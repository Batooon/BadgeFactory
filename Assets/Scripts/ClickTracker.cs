using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Developer: Antoshka

public class ClickTracker : MonoBehaviour
{
    public Camera UICamera;
    public GameObject HitEffect;

    private void Update()
    {
        Touch[] touches = Input.touches;
        if (touches.Length > 0)
        {
            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i].phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(UICamera.ScreenToWorldPoint(touches[i].position), Vector2.zero);
                    if (hit.transform.GetComponent<ClickTracker>() != null)
                    {
                        Vector3 effectPosition = UICamera.ScreenToWorldPoint(Input.GetTouch(i).position);
                        effectPosition.z = 80f;
                        GameObject effect = Instantiate(HitEffect, effectPosition, Quaternion.identity);
                        Destroy(effect, 0.7f);
                    }
                }
            }
        }
    }
}
