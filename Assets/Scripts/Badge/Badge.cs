using Badge.BusinessRules;
using DroppableItems;
using System.Collections.Generic;
using UnityEngine;

namespace Badge
{
    public class Badge : MonoBehaviour
    {
        public List<Sprite> Badges;
        public List<DroppableObject> _droppableItems;

        private IBadgeBusinessInput _badgeBusinessInput;
        private IBadgeBusinessOutput _badgeOutput;

        private void Awake()
        {
            BadgePresentation badgePresentation = GetComponent<BadgePresentation>();
            _badgeOutput = new BadgePresentator(badgePresentation, _droppableItems);
            _badgeBusinessInput = new BadgeBusinessRules(PlayerDataAccess.GetPlayerDatabase(),
                                                         BadgeDatabaseAccess.GetBadgeDatabase(),
                                                         _badgeOutput);
        }

        private void Update()
        {
            //_badgeBusinessInput.TakeProgress();
        }
    }
}