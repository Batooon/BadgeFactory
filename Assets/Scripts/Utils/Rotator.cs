using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _anglesToRotateEverySecond;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.Rotate(_anglesToRotateEverySecond * Time.deltaTime);
    }
}
