using System.Collections.Generic;
using MushyAndCoffe.Interfaces;
using MushyAndCoffe.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace MushyAndCoffe.CraftingSystem
{
	public abstract class Machine : MonoBehaviour, IInteractable
	{
		public MachineSO MachineSO;
		public List<IngredientSO> IngredientsStored;
		
		public bool StoreIngredient(IngredientSO ingredient) 
		{
			if (IngredientsStored.Contains(ingredient)) return false;
			
			IngredientsStored.Add(ingredient);
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
		
		public void Interact() 
		{
			CraftingManager.Instance.CraftRecipe(this);
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(Machine), true)]
	public class MachineEditor : Editor 
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			var machine = (Machine)target;
			
			if (GUILayout.Button("Craft")) 
			{
				machine.Interact();
			}
		}	
	}
#endif
}
