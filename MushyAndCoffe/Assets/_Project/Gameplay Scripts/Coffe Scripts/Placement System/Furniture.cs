using MushyAndCoffe.Interfaces;
using MushyAndCoffe.ScriptableObjects;
using UnityEngine;

namespace MushyAndCoffe
{
    public class Furniture : MonoBehaviour, IInteractable, ISOContainer
    {
        [SerializeField] private FurnitureSO furnitureSO;
        public ScriptableObject ScriptableObject { get {return furnitureSO;} set {} }

        public void Interact(GameObject playerObject)
        {
            throw new System.NotImplementedException();
        }
    }
}
