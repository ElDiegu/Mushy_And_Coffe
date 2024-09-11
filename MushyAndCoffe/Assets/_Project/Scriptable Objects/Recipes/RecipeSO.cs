using System.Collections.Generic;
using UnityEngine;

namespace MushyAndCoffe.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Recipe", menuName = "Mushy And Coffe/Recipe")]
	public class RecipeSO : ScriptableObject
	{
		public int RecipeID;
		public string RecipeName;
		public List<IngredientSO> RecipeIngredients;
		public ScriptableObject RecipeResult;
		public MachineSO RecipeMachine;
	}
}
