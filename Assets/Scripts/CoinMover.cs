using UnityEngine;
using DroppableItems;
using UnityEngine.Events;

public class CoinMover : MonoBehaviour, IItemTweener
{
    [SerializeField]
    private UnityEvent _finished;

    private Vector3 _destination;

    public void StartMotion()
    {
        LeanTween.move(gameObject,
            new Vector2(Random.Range(-1.5f, 1.5f),
            Random.Range(-1.5f, 1.5f)),
            .5f).setOnComplete(Collect);
    }

    private void Collect()
    {
        LeanTween.move(gameObject, _destination, .5f).setOnComplete(_finished.Invoke);
        LeanTween.scale(gameObject, Vector2.zero, .5f);
    }

    public void SetDestination(Vector2 destination)
    {
        _destination = destination;
    }
}
