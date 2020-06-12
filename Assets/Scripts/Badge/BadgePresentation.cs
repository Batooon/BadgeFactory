using UnityEngine;

namespace Badge
{
    public class BadgePresentation : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _badgeSprite;

        public void UpdateBadgeProgress(float alpha)
        {
            Color tempColor = _badgeSprite.color;
            tempColor.a = alpha;

            _badgeSprite.color = tempColor;
        }

        public void ShowNewBadge(Sprite sprite)
        {
            _badgeSprite.sprite = sprite;
        }
    }

    public interface IClickEffect
    {
        void SpawnEffect(Vector2 position);
    }
}