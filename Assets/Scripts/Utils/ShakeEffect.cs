using System.Collections;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _magnitude;
    [SerializeField] private Vector3 _originalPosition = new Vector3();

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public void StartShaking()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        _transform.localPosition = _originalPosition;

        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            float x = Random.Range(-1f, 1f) * _magnitude;
            float y = Random.Range(-1f, 1f) * _magnitude;

            _transform.localPosition = new Vector3(x, y, _originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _transform.localPosition = _originalPosition;
    }
}
