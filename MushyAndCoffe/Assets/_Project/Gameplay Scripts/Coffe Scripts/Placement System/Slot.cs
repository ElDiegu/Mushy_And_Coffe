using MushyAndCoffe.PlacementSystem;
using MushyAndCoffe.ScriptableObjects;
using EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MushyAndCoffe
{
    public class Slot : MonoBehaviour
    {
        public Furniture Furni { get; set; }
        public struct OnPlaceSlotEvent: IEvent
        {
            public Sprite icon;
            public string name;
            public string description;
        }

        public void ChangeCurrentItem()
        {
            EventBus<OnPlaceSlotEvent>.Raise(new OnPlaceSlotEvent()
            {
                icon = (Furni.ScriptableObject as FurnitureSO).Icon,
                name = (Furni.ScriptableObject as FurnitureSO).Name,
                description = (Furni.ScriptableObject as FurnitureSO).Description
            });
            PlacementManager.Instance.ChangePreviewObject(Furni.gameObject);
        }
    }

}
