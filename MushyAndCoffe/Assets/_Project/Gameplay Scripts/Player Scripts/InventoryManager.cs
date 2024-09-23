using MushyAndCoffe.Events;
using MushyAndCoffe.Managers;
using MushyAndCoffe.Systems.EventSystem;
using UnityEngine;

namespace MushyAndCoffe.Player
{
    public class InventoryManager : MonoBehaviour
    {
        public Transform grabTransform;
        public GameObject item;
        
        EventBinding<PickUpEvent> pickUpEventBinding;
        
        private void OnEnable()
        {
            pickUpEventBinding = new EventBinding<PickUpEvent>(PickUpObject);
            EventBus<PickUpEvent>.Register(pickUpEventBinding);
        }
        
        private void OnDisable()
        {
            EventBus<PickUpEvent>.Deregister(pickUpEventBinding);
        }
        
        private void PickUpObject(PickUpEvent pickUpEvent) 
        {
            DebugManager.Log(MessageTypes.Interaction, $"Picking up {pickUpEvent.pickedObject.name} object");
            
            item = pickUpEvent.pickedObject;
            
            item.transform.SetParent(grabTransform, false);
            item.transform.localPosition = Vector3.zero;       
        }
    }
}
