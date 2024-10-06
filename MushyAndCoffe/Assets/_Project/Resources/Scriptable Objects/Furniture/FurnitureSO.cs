using UnityEngine;

namespace MushyAndCoffe.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Furniture", menuName = "Mushy And Coffe/Furniture")]
	public class FurnitureSO : ScriptableObject
	{
		[field: SerializeField]      
		public int ID { get; private set; }

		[field: SerializeField]
		public string Name { get; private set;}

		[field: SerializeField]
		public Vector3 Size { get; private set; }

		[field: SerializeField]
		public GameObject Prefab { get; set; }
		
		[field: SerializeField]
		public FurnitureSurface Surface { get; private set; }
		
		public enum FurnitureSurface 
		{
			Floor,
			Wall,
			Ceiling
		}
	}
}
