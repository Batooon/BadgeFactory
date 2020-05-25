using System;
using System.Collections.Generic;
using UnityEngine;

namespace Badge
{
    public class BadgePresentation : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _badgeSprite;
        /*[SerializeField]
        private GameObject _coin;*/

        public void UpdateBadgeProgress(float alpha)
        {
            Color tempColor = _badgeSprite.color;
            tempColor.a = alpha;

            _badgeSprite.color = tempColor;
        }

        /*public void SpawnCoin()
        {
            GameObject spawnedCoin = Instantiate(_coin, transform.position, Quaternion.identity);
            LeanTween.move(spawnedCoin, new Vector2(UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f)), .5f);
        }*/

        public void ShowNewBadge(Sprite sprite)
        {
            _badgeSprite.sprite = sprite;
        }

        public void Drop(GameObject item)
        {
            GameObject itemToSpawn = Instantiate(item, transform.position, Quaternion.identity);
        }
    }

    public interface IClickEffect
    {
        void SpawnEffect(Vector2 position);
    }
}