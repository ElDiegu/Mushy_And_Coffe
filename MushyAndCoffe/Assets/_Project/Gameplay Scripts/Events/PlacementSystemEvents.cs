using MushyAndCoffe.Systems.EventSystem;
using UnityEngine;

namespace MushyAndCoffe.Events
{
    public struct SelectFurnitureEvent : IEvent
    {
        public GameObject selectedFurniture;
    }
}
