using DroppableItems;
using UnityEngine;

public class UsualItemMover : MonoBehaviour, IItemTweener
{
    public void StartMotion()
    {
        LeanTween.move(gameObject, new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f)), .5f);
    }
}
