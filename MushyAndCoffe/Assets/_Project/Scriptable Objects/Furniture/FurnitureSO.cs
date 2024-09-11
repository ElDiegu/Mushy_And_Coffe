using UnityEngine;

namespace MushyAndCoffe.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Machine", menuName = "Mushy And Coffe/Machine")]
	public class FurnitureSO : ScriptableObject
	{
		public int FurnitureID;
		public string FurnitureName;
	}
}
