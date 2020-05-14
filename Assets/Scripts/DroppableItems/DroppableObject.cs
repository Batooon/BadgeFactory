using Badge;
using OdinSerializer;

namespace DroppableItems
{
    public class DroppableObject : SerializedMonoBehaviour
    {
        public int ChanceToSpawn = 100;
        public float Lifetime = 20f;

        private IDroppable _droppableItem;
        private IItemTweener _mover;
        private IPlayerDataProvider _playerDataProvider;

        public int GetAmountToSpawn(BadgeData badgeData)
        {
            return _droppableItem.AmountToSpawn(badgeData);
        }

        private void OnMouseEnter()
        {
            Data playerData = _playerDataProvider.GetPlayerData();
            _droppableItem.Collect(ref playerData);
            _playerDataProvider.SavePlayerData(in playerData);
            Destroy(gameObject);
        }

        private void Awake()
        {
            _mover = GetComponent<IItemTweener>();
            _droppableItem = GetComponent<IDroppable>();
            _playerDataProvider = PlayerDataAccess.GetPlayerDatabase();
        }

        private void Start()
        {
            _mover.StartMotion();
            Destroy(gameObject, Lifetime);
        }
    }
}