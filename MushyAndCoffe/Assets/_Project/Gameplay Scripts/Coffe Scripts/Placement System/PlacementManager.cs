using MushyAndCoffe.Extensions;
using MushyAndCoffe.Managers;
using UnityEngine;

namespace MushyAndCoffe.PlacementSystem
{
	public class PlacementManager : Singleton<PlacementManager>
	{
		[SerializeField] private GameObject selectedObject;
		[SerializeField] private Grid grid;
		[SerializeField] private LayerMask placementLayerMask;
		[SerializeField] private Camera usedCamera;
		[SerializeField] private GameObject previewObject = null;
		private Physics physics;
		private Quaternion objectRotation = new Quaternion();

		private void Update()
		{
			var input = InputManager.Instance.GetInput();

			if (input.LeftClick) PlaceObject();
			
			if (input.RotateObject) RotateObject(90);
			
			UpdatePreview();
		}
		
		private void PlaceObject() 
		{
			bool hit = physics.MouseRaycastFromCamera(Input.mousePosition, usedCamera, 100f, placementLayerMask, out Vector3 clickLocation);
			
			if (!hit) return;
			
			var cell = grid.WorldToCell(clickLocation);
			
			Debug.Log($"{clickLocation} | {cell}");
			
			Instantiate(selectedObject, grid.GetCellCenterWorld(cell), objectRotation);
		}
		
		private void RotateObject(float degrees) 
		{
			if (objectRotation.y + degrees >= 360) objectRotation.y = 0f;   
			else objectRotation = Quaternion.Euler(objectRotation.eulerAngles.x, objectRotation.eulerAngles.y + degrees, objectRotation.eulerAngles.z);
		}
		
		private void UpdatePreview() 
		{
			bool hit = physics.MouseRaycastFromCamera(Input.mousePosition, usedCamera, 100f, placementLayerMask, out Vector3 clickLocation);
			
			if (!hit || selectedObject == null) 
			{
				if (previewObject != null) Destroy(previewObject);
				return;	
			}
			
			if (previewObject == null) previewObject = Instantiate(selectedObject, clickLocation, new Quaternion());
			
			var cell = grid.WorldToCell(clickLocation);
			
			previewObject.transform.position = grid.GetCellCenterWorld(cell);
			previewObject.transform.rotation = objectRotation;
		}
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
