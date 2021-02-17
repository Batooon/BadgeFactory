using System;
using UnityEngine;

public enum FOVAxis
{
    Horizontal,
    Vertical
}

[RequireComponent(typeof(Camera))]
public class ScaleFormat : MonoBehaviour
{
    [SerializeField] private FOVAxis _fovAxis;
    [SerializeField] private float _preferredOppositeFOV;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        switch (_fovAxis)
        {
            case FOVAxis.Horizontal:
                _camera.fieldOfView = GetVerticalFOV(_preferredOppositeFOV, _camera.aspect);
                break;
            case FOVAxis.Vertical:
                _camera.fieldOfView = GetHorizontalFOV(_preferredOppositeFOV, _camera.aspect);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private float GetVerticalFOV(float hFOVDeg, float aspect)
    {
        float hFOVRad = hFOVDeg * Mathf.Deg2Rad;
        float vFOVRad = 2 * Mathf.Atan(Mathf.Tan(hFOVRad * .5f) / aspect);
        return vFOVRad * Mathf.Rad2Deg;
    }

    private float GetHorizontalFOV(float vFOVDeg, float aspect)
    {
        float vFOVRad = vFOVDeg * Mathf.Deg2Rad;
        float hFOVRad = 2 * Mathf.Atan(aspect * Mathf.Tan(vFOVRad * .5f));
        return hFOVRad * Mathf.Rad2Deg;
    }
}