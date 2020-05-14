using System.Collections.Generic;
using UnityEngine;

namespace DroppableItems
{
    public class DroppableObjectPresentation : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _droppableObjects;

        private SpriteRenderer _sprite;
    }
}