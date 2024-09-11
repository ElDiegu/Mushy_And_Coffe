using UnityEngine;

namespace MushyAndCoffe.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Ingredient", menuName = "Mushy And Coffe/Ingredient")]
	public class IngredientSO : ScriptableObject
	{
		public int IngredientID;
		public string IngredientName;

	}
}
