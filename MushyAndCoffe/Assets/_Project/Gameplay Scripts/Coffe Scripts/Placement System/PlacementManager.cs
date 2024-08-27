using MushyAndCoffe.Extensions;
using MushyAndCoffe.Managers;
using UnityEngine;

namespace MushyAndCoffe.PlacementSystem
{
	public class PlacementManager : Singleton<PlacementManager>
	{
		[SerializeField] private GameObject testObject;
		[SerializeField] private Grid grid;
		[SerializeField] private LayerMask placementLayerMask;
		[SerializeField] private Camera usedCamera;
		private Physics physics;

		private void Update()
		{
			var input = InputManager.Instance.GetInput();

			if (!input.LeftClick) return;
			
			bool hit = physics.MouseRaycastFromCamera(Input.mousePosition, usedCamera, 100f, placementLayerMask, out Vector3 clickLocation);
			
			if (!hit) return;
			
			var cell = grid.WorldToCell(clickLocation);
			
			Debug.Log($"{clickLocation} | {cell}");
			
			Instantiate(testObject, grid.CellToWorld(cell) + new Vector3(grid.cellSize.x / 2, grid.cellSize.y / 2, 0.0f), new Quaternion());
		}
		
#if UNITY_EDITOR
		private void OnDrawGizmos() 
		{
			Gizmos.color = Color.red;
			
			bool hit = physics.MouseRaycastFromCamera(Input.mousePosition, usedCamera, 100f, placementLayerMask, out Vector3 clickLocation);
			
			if (hit) 
			{
				Gizmos.DrawSphere(clickLocation, 0.2f);
			}
		}
#endif
	}
}
