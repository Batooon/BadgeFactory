using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScaleFormat : MonoBehaviour
{
    [SerializeField] private float _preferredHFOV;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.fieldOfView = GetVerticalFOV(_preferredHFOV, _camera.aspect);
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