using UnityEngine;

namespace Badge
{
    public class ClickPresentation : MonoBehaviour, IClickEffect
    {
        [SerializeField]
        private GameObject _particleEffect;

        public void SpawnEffect(Vector2 position)
        {
            Debug.Log("Spawned");
            GameObject effect = Instantiate(_particleEffect, position, Quaternion.identity);
            Destroy(effect, .7f);
        }
    }
}