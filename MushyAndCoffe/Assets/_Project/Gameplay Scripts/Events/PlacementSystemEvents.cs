using MushyAndCoffe.ScriptableObjects;
using EventSystem;
using UnityEngine;

namespace MushyAndCoffe.Events
{
    public struct OnSelectFurnitureEvent : IEvent
    {
        public GameObject selectedFurniture;
    }
    
    public struct OnPlaceFurnitureEvent : IEvent 
    {
        public Vector3Int location;
        public GameObject furnitureObject;
        public FurnitureSO furnitureData;
    }
}
