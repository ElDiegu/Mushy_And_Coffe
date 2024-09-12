using System.Collections.Generic;
using MushyAndCoffe.Databases;
using MushyAndCoffe.Managers;
using MushyAndCoffe.ScriptableObjects;
using UnityEngine;

namespace MushyAndCoffe.CraftingSystem
{
	public class CraftingManager : StaticInstance<CraftingManager>
	{
		public ScriptableObject CraftRecipe(Machine machine) 
		{
			var recipeToCraft = RecipesDatabase.FindRecipe(machine.IngredientsStored, machine.MachineSO);
			
			DebugManager.Log(MessageTypes.Crafting, $"{recipeToCraft}");
			
			if (recipeToCraft == null) return null;
			
			return recipeToCraft.RecipeResult;
		}
	}
}
