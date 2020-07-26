using UnityEngine;

namespace DroppableItems
{
    public class EffectSpawner : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effect;

        public void SpawnEffect(Vector3 position)
        {
            Instantiate(_effect.gameObject, position, Quaternion.identity);
            if (_effect.IsAlive() == false)
            {
                Destroy(gameObject);
            }
        }
    }
}