using UnityEngine;

namespace DroppableItems
{
    public class Coin : MonoBehaviour, ICollectable
    {
        [SerializeField] private ParticleSystem _collectEffect;

        private PlayerData _playerData;
        private IItemTweener _itemTweener;
        private Transform _transform;

        private int _costReward;

        public void Init(in int reward, PlayerData playerData)
        {
            _costReward = reward;
            _playerData = playerData;
            _itemTweener = GetComponent<IItemTweener>();
            _transform = GetComponent<Transform>();
            _itemTweener.StartMotion();
        }

        public void OnMovingEnded()
        {
            GameObject effect = Instantiate(_collectEffect.gameObject, _transform.position, _transform.rotation);
            Destroy(effect, _collectEffect.main.duration);
            Collect();
            Destroy(gameObject);
        }

        private void Collect()
        {
            _playerData.Gold += _costReward;
        }
    }

    [System.Serializable]
    public struct AmountOfObjectsToSpawn
    {
        public int MinValue;
        public int MaxValue;
    }
}