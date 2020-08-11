using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScaleFormat : MonoBehaviour
{
    [SerializeField] private float _preferredHFOV;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.fieldOfView = GetVerticalFOV(_preferredHFOV);
    }

    private float GetVerticalFOV(float hFOVDeg)
    {
        float hFOVRad = hFOVDeg * Mathf.Deg2Rad;
        float vFOVRad = 2 * Mathf.Atan(Mathf.Tan(hFOVRad * .5f) / _camera.aspect);
        return vFOVRad * Mathf.Rad2Deg;
    }
}