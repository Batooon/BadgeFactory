using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CoinView : MonoBehaviour, IPointerEnterHandler
{
    public CoinCollectedCallback CoinCollected;
    public CollectedCoinTextSpawned TextSpawnedCallback;

    [HideInInspector]
    public bool CanTextMove = false;

    [SerializeField]
    private GameObject _collectedCoinText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        CoinCollected?.Invoke();
        //Destroy(gameObject);
    }

    public void StartMotion()
    {
        LeanTween.move(gameObject, new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f)), .5f);
    }

    public void SpawnCollectedText(int amount)
    {
        GameObject collectedCoinText = Instantiate(_collectedCoinText, transform.position,
            Quaternion.identity, transform.parent);
        collectedCoinText.GetComponent<TextMeshProUGUI>().text = $"+{amount.ConvertValue()}";
        TextSpawnedCallback?.Invoke(collectedCoinText);
    }

    public void StartTextMotion(GameObject text, float speed, float lifetime)
    {
        LeanTween.moveY(text, text.transform.position.y + speed * lifetime, lifetime).setDestroyOnComplete(true);
    }
}
