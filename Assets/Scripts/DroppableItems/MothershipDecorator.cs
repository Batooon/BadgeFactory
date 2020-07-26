using BadgeImplementation;
using System.Collections.Generic;
using UnityEngine;

namespace DroppableItems
{
    public abstract class MothershipDecorator : DroppingMothership
    {
        [SerializeField]
        protected List<DroppingMothership> _droppingMotherships;

        public override void Spawn(Vector3 position)
        {
            foreach (var mothership in _droppingMotherships)
            {
                mothership.Spawn(position);
            }
        }
    }
}
