using System.Collections.Generic;
using UnityEngine;

namespace MushyAndCoffe.PlacementSystem
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [field: SerializeField] public List<Furniture> Furnitures { get; private set; } = new List<Furniture>();
        
        
        public void AddFurniture(Furniture furni) => Furnitures.Add(furni);
        public void RemoveFurniture(Furniture furni) => Furnitures.Remove(furni); 

    }
}
