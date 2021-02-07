using UnityEngine;
using UnityEngine.Events;

namespace DroppableItems
{
    public class Coin : MonoBehaviour, ICollectable
    {
        [SerializeField] private Sound _collectSound;
        [SerializeField] private ParticleSystem _collectEffect;
        [SerializeField] private UnityEvent _collect;

        private AudioService _audioService;
        private PlayerData _playerData;
        private IItemTweener _itemTweener;
        private Transform _transform;

        private long _costReward;

        public void Init(in long reward, PlayerData playerData, AudioService audioService)
        {
            _audioService = audioService;
            _costReward = reward;
            _playerData = playerData;
            _itemTweener = GetComponent<IItemTweener>();
            _transform = GetComponent<Transform>();
            _itemTweener.StartMotion();
        }

        public void OnMovingEnded()
        {
            if (_playerData.IsProgressResetting)
            {
                gameObject.SetActive(false);
                return;
            }
            PlaySound();
            _collect?.Invoke();
            GameObject effect = Instantiate(_collectEffect.gameObject, _transform.position, _transform.rotation);
            Destroy(effect, _collectEffect.main.duration);
            Collect();
            gameObject.SetActive(false);
        }

        public void PlaySound()
        {
            _audioService.Play(_collectSound);
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