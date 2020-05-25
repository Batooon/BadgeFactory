using Badge;
using OdinSerializer;
using System;
using System.Collections.Generic;

namespace DroppableItems
{
    [Serializable]
    public class DroppableObject : SerializedMonoBehaviour
    {
        public int ChanceToSpawn = 100;
        public float Lifetime = 20f;

        /*[SerializeReference]
        [SelectImplementation(typeof(IDroppable))]*/
        [OdinSerialize]
        private IDroppable _droppableItem;

        //TODO: это тоже нужно вынести в личные классы объектов
        private IItemTweener _mover;
        private IPlayerDataProvider _playerDataProvider;

        //TODO: сделать базовый клас для функции дропа
        //TODO: хранить тут кого нужно создавать List<DroppableObject>

        /*public void SetDroppableItemType(ICollectable droppable)
        {
            _droppableItem=droppable;
        }*/

        public IEnumerable<ICollectable> GetAmountToSpawn(BadgeData badgeData)
        {
            return _droppableItem.GetObjectsToSpawn(badgeData);
            //return _droppableItem.AmountToSpawn(badgeData);
        }

        //TODO: вынести этот метод в собственные монобех классы (Coin:MonoBehaviour)
        /*private void OnMouseEnter()
        {
            Data playerData = _playerDataProvider.GetPlayerData();
            _droppableItem.Collect(ref playerData);
            _playerDataProvider.SavePlayerData(in playerData);
            Destroy(gameObject);
        }*/

        private void Awake()
        {
            _mover = GetComponent<IItemTweener>();
            _playerDataProvider = PlayerDataAccess.GetPlayerDatabase();
        }

        private void Start()
        {
            _mover.StartMotion();
            Destroy(gameObject, Lifetime);
        }
    }
}