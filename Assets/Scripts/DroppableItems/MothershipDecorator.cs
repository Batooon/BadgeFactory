using Badge;
using System.Collections.Generic;
using UnityEngine;

namespace DroppableItems
{
    public abstract class MothershipDecorator : DroppingMothership
    {
        [SerializeField]
        protected List<DroppingMothership> _droppingMotherships;

        /*public void SetMotherShip(DroppingMothership mothership)
        {
            _droppingMotherships = mothership;
        }*/

        public override void Spawn(Vector2 position)
        {
            foreach (var mothership in _droppingMotherships)
            {
                mothership.Spawn(position);
            }
        }
    }
}
