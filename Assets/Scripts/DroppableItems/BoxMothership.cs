using UnityEngine;
using UnityEngine.Events;

namespace DroppableItems
{
    public class BoxMothership : DroppingMothership
    {
        [SerializeField] private UnityEvent _boxOpenEvent;
        [SerializeField] private BoxPromtPanelPresentation _promtPanel;

        public override void Spawn(Vector3 position)
        {
            if (Random.value <= _chanceToSpawn) 
            {
                GameObject boxObject = Instantiate(_itemToSpawn, transform.position, Quaternion.identity);
                Box box = boxObject.GetComponent<Box>();
                _promtPanel.Init(_badgeData.CoinsReward * 20, _playerData, _badgeData);
                box.Init(_boxOpenEvent);
            }
        }
    }
}
