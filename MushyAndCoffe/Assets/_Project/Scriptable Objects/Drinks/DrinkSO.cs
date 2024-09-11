using System.Collections.Generic;
using MushyAndCoffe.ScriptableObjects;
using UnityEngine;

namespace MushyAndCoffe
{
	[CreateAssetMenu(fileName = "Drink", menuName = "Mushy And Coffe/Drink")]
	public class DrinkSO : ScriptableObject
	{
		public int DrinkID;
		public string DrinkName;
		public List<IngredientSO> DrinkComponents;
		public int DrinkScore;
	}
}
