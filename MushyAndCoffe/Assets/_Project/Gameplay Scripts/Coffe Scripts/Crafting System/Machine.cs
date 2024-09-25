using System.Collections.Generic;
using MushyAndCoffe.Interfaces;
using MushyAndCoffe.Items;
using MushyAndCoffe.Managers;
using MushyAndCoffe.Player;
using MushyAndCoffe.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace MushyAndCoffe.CraftingSystem
{
    public class Machine : MonoBehaviour, IInteractable, ISOContainer, IActivableUI
    {
        [SerializeField]
        private MachineSO machineSO;
        public ScriptableObject ScriptableObject { get {return machineSO;} set {} }
        
        [SerializeField]
        private GameObject activableInterface;
        public GameObject ActivableInterface { get {return activableInterface;} set {}}

        public List<IngredientSO> IngredientsStored;
        
        public void Interact(GameObject playerObject) 
        {
            var playerInventory = playerObject.GetComponent<InventoryManager>();
            
            if (machineSO.AllowedIngredients.Contains(playerInventory.item.GetComponent<ISOContainer>().ScriptableObject)) StoreIngredient(playerInventory.item);
            else DebugManager.Log(MessageTypes.Crafting, "Invalid");
        }
        
        public void SetInterfaceVisivility(bool state) 
        {
            ActivableInterface.SetActive(state);
        }

        public bool StoreIngredient(GameObject ingredientObject) 
        {
            var ingredientSO = (IngredientSO) ingredientObject.GetComponent<ISOContainer>().ScriptableObject;
            
            if (IngredientsStored.Contains(ingredientSO)) return false;
            
            IngredientsStored.Add(ingredientSO);
            Destroy(ingredientObject);
            
            return true;
        }
        
        public bool RemoveIngredient(IngredientSO ingredient) 
        {
            return IngredientsStored.Remove(ingredient);
        }
        
        public void RemoveAllIngredients() 
        {
            IngredientsStored.Clear(); 
        }
        
        
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Machine), true)]
    public class MachineEditor : Editor 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            /*         
            var machine = (Machine)target;
            
            if (GUILayout.Button("Craft")) 
            {
                machine.Interact(null);
            }
            */
        }	
    }
#endif
}
