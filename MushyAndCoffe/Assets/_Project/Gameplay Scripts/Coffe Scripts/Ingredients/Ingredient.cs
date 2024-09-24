using MushyAndCoffe.Events;
using MushyAndCoffe.Interfaces;
using MushyAndCoffe.Managers;
using MushyAndCoffe.ScriptableObjects;
using MushyAndCoffe.Systems.EventSystem;
using UnityEngine;

namespace MushyAndCoffe.Items
{
    public class Ingredient : MonoBehaviour, IInteractable, ISOContainer
    {
        [SerializeField]
        private IngredientSO ingredientSO;
        public ScriptableObject InteractableType { get {return ingredientSO;} set {} }

        public void Interact(GameObject playerObject)
        {
            DebugManager.Log(MessageTypes.Interaction, $"Interacted with {gameObject.name}");
            EventBus<PickUpEvent>.Raise(new PickUpEvent() 
            {
                pickedObject = gameObject
            });
            
            this.GetComponent<Collider>().enabled = false;         
        }
    }
}
