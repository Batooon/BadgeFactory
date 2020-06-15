using Badge;
using System.Collections.Generic;
using UnityEngine;

namespace DroppableItems
{
    public class BoxMothership : MothershipDecorator
    {
        public override void Spawn(Vector2 position)
        {
            if (Random.Range(0, 101) <= _chanceToSpawn)
            {
                GameObject boxObj = Instantiate(_itemToSpawn, transform.position, Quaternion.identity);
                Box box = boxObj.GetComponent<Box>();
                box.BoxOpened += OnBoxOpened;
            }
        }

        private void OnBoxOpened(Vector2 boxPosition)
        {
            base.Spawn(boxPosition);
        }
    }
}
