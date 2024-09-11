using System.Collections.Generic;
using System.Linq;
using MushyAndCoffe.ScriptableObjects;
using UnityEngine;

namespace MushyAndCoffe.Databases
{
	public static class RecipesDatabase
	{
		public static List<RecipeSO> Recipes { get; set; } = Resources.LoadAll<RecipeSO>("Scriptable Objects/Recipes").ToList();
		  
		public static RecipeSO FindByName(string name) 
		{
			return Recipes.Where(recipe => recipe.RecipeName == name).FirstOrDefault();
		}
		
		public static RecipeSO FindByID(int ID) 
		{
			return Recipes.Where(recipe => recipe.RecipeID == ID).FirstOrDefault();
		}

		public static List<RecipeSO> FindByMachine(MachineSO machine) 
		{
			return Recipes.Where(recipe => recipe.RecipeMachine == machine).ToList();
		}
		
		public static RecipeSO FindRecipe(List<IngredientSO> ingredients, MachineSO machine = null) 
		{			
			var recipesToCheck = Recipes.Where(recipe => recipe.RecipeIngredients.Count == ingredients.Count).ToList();
			
			if (machine) recipesToCheck = recipesToCheck.Where(recipe => recipe.RecipeMachine == machine).ToList();
			
			var sortedIngredients = ingredients.OrderBy(ingredient => ingredient.IngredientID).ToList();
			
			foreach (RecipeSO recipe in recipesToCheck) 
			{
				var recipeIngredients = recipe.RecipeIngredients.OrderBy(ingredient => ingredient.IngredientID).ToList();
				
				for (int i = 0; i < ingredients.Count; i++) 
				{
					if (recipeIngredients[i] != sortedIngredients[i]) break;
				}
				
				return recipe;
			}
			
			return null;
		}
	}
}
