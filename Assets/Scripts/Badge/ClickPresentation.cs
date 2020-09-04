using UnityEngine;

namespace Badge
{
    public class ClickPresentation : MonoBehaviour, IClickEffect
    {
        [SerializeField]
        private GameObject _particleEffect;
        [SerializeField]
        private float _lifetime;

        public void SpawnEffect(Vector2 position)
        {
            GameObject effect = Instantiate(_particleEffect, position, Quaternion.identity);
            Destroy(effect, _lifetime);
        }
    }
}