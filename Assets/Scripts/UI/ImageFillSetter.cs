using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFillSetter : MonoBehaviour
{
    public FloatReference Variable;

    public FloatReference Min;

    public FloatReference Max;

    public Image Image;

    public void UpdateHealth()
    {
        Image.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(Min, Max, Variable));
    }
}
