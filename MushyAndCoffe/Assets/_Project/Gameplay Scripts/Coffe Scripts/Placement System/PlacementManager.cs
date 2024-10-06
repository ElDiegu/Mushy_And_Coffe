using System.Collections.Generic;
using MushyAndCoffe.Events;
using MushyAndCoffe.Extensions;
using MushyAndCoffe.Managers;
using MushyAndCoffe.ScriptableObjects;
using MushyAndCoffe.Systems.EventSystem;
using UnityEngine;
using UnityEngine.Rendering;

namespace MushyAndCoffe.PlacementSystem
{
	public class PlacementManager : Singleton<PlacementManager>
	{
		[SerializeField] private GameObject selectedObject, cellSelected;
		[SerializeField] private Grid grid;
		[SerializeField] private LayerMask placementLayerMask;
		[SerializeField] private Camera usedCamera;
		[SerializeField] private GameObject previewObject = null;
		[SerializeField] private GameObject cellSelector = null;
		private Physics physics;
		
		[SerializeField] private SerializedDictionary<Vector3Int, GameObject> cellInformation = new SerializedDictionary<Vector3Int, GameObject>();
		
#region Events      
		EventBinding<SelectFurnitureEvent> selectFurnitureEvent;
#endregion
		
		
		
#region Unity Methods
		private void OnEnable()
		{
			selectFurnitureEvent = new EventBinding<SelectFurnitureEvent>(ChangeSelectedObject);
			EventBus<SelectFurnitureEvent>.Register(selectFurnitureEvent);
		}

		private void Update()
		{
			var input = InputManager.Instance.GetInput();

			if (input.LeftClick) PlaceObject();
			
			if (input.RotateObject) RotateObject(90);
			
			UpdatePreview();
		}
#endregion
		
		private void PlaceObject() 
		{
			bool hit = physics.MouseRaycastFromCamera(Input.mousePosition, usedCamera, 100f, placementLayerMask, out Vector3 clickLocation);
			
			if (!hit) return;
			
			var cell = grid.WorldToCell(clickLocation);
			var furnitureSO = (FurnitureSO) selectedObject.GetComponent<Furniture>().ScriptableObject;
			
			if (!CheckIfPlacementAllowed(furnitureSO, cell)) return;
			
			var furnitureCreated = Instantiate(selectedObject, previewObject.transform.position, previewObject.transform.rotation);
			
			OccupyCells(GetOccupiedCells(furnitureSO, cell), furnitureCreated);
		}
		
		private void RotateObject(float degrees) 
		{
			// We can't rotate wall objects
			if (((FurnitureSO)selectedObject.GetComponent<Furniture>().ScriptableObject).Surface == FurnitureSO.FurnitureSurface.Wall) return;
			
			// We can't rotate the preview if it doesn't exists
			if (previewObject == null) return;
			
			var previewRotation = previewObject.transform.rotation;
			
			if (previewRotation.y + degrees >= 360) previewRotation.y = 0f;
			else previewRotation = Quaternion.Euler(previewRotation.eulerAngles.x, previewRotation.eulerAngles.y + degrees, previewRotation.eulerAngles.z);
		}
		
		private void ChangeSelectedObject(SelectFurnitureEvent selectFurnitureEvent) 
		{
			selectedObject = selectFurnitureEvent.selectedFurniture;
		}

#region Object preview
		private void UpdatePreview() 
		{
			bool hitDetected = physics.MouseRaycastFromCamera(Input.mousePosition, usedCamera, 100f, placementLayerMask, out RaycastHit hit);
			
			var clickLocation = hit.point;
			
			if (!hitDetected || selectedObject == null) 
			{
				ClearPreview();
				return;	
			}
			
			var furnitureSurface = ((FurnitureSO)selectedObject.GetComponent<Furniture>().ScriptableObject).Surface;
			
			if (furnitureSurface == FurnitureSO.FurnitureSurface.Floor) UpdateFloorPreview(hit, clickLocation);
			
			if (furnitureSurface == FurnitureSO.FurnitureSurface.Wall) UpdateWallPreview(hit, clickLocation);
			
			UpdateCellSelector(hit, clickLocation);
		}
		
		private void UpdateFloorPreview(RaycastHit hit, Vector3 clickLocation) 
		{
			if (hit.normal != Vector3.up) return;
			
			if (previewObject == null) previewObject = Instantiate(selectedObject, clickLocation, new Quaternion());
			
			var cell = grid.WorldToCell(clickLocation);

			previewObject.transform.position = grid.GetCellCenterWorld(cell) - new Vector3(0f, 0.5f, 0f);
		}
		
		private void UpdateWallPreview(RaycastHit hit, Vector3 clickLocation) 
		{
			if (hit.normal == Vector3.up) return;
			
			if (previewObject == null) previewObject = Instantiate(selectedObject, clickLocation, Quaternion.LookRotation(hit.normal));

			var cell = grid.WorldToCell(clickLocation);

			previewObject.transform.position = grid.GetCellCenterWorld(cell) - new Vector3(0f, 0.5f, 0f);
			previewObject.transform.rotation = Quaternion.LookRotation(hit.normal);
		}
		
		private void UpdateCellSelector(RaycastHit hit, Vector3 clickLocation) 
		{
			if (cellSelector == null) cellSelector = Instantiate(cellSelected, clickLocation, new Quaternion());
			
			var cell = grid.WorldToCell(clickLocation);
			
			cellSelector.transform.position = grid.GetCellCenterWorld(cell) - new Vector3(0f, 0.5f, 0f);
		}
		
		private void ClearPreview() 
		{
			if (previewObject != null) Destroy(previewObject);
			if (cellSelector != null) Destroy(cellSelector);
		}
#endregion

#region Grid info storage		
		private List<Vector3Int> GetOccupiedCells(FurnitureSO furniture, Vector3Int cell) 
		{
			var cells = new List<Vector3Int>();
			
			for (int i = 0; i < furniture.Size.x; i++) 
			{
				for (int j = 0; j < furniture.Size.y; j++) 
				{
					cells.Add(new Vector3Int(cell.x + i, cell.y, cell.z + j));
				}
			}
			
			return cells;
		}
		
		private bool CheckIfPlacementAllowed(FurnitureSO furniture, Vector3Int cell) 
		{
			var occupiedCells = GetOccupiedCells(furniture, cell);
			
			foreach (Vector3Int key in cellInformation.Keys) 
			{
				if (occupiedCells.Contains(key)) return false;
			}
			
			return true;
		}
		
		private void OccupyCells(List<Vector3Int> occupiedCells, GameObject occupyingObject) 
		{
			foreach (Vector3Int occupiedCell in occupiedCells) cellInformation.Add(occupiedCell, occupyingObject);
		}
#endregion
		
#if UNITY_EDITOR
		private void OnDrawGizmos() 
		{
			Gizmos.color = Color.red;
			
			bool hit = physics.MouseRaycastFromCamera(Input.mousePosition, usedCamera, 100f, placementLayerMask, out RaycastHit raycastHit);
			
			if (hit) 
			{
				Gizmos.DrawSphere(raycastHit.point, 0.2f);
				Gizmos.DrawLine(raycastHit.point, raycastHit.point + raycastHit.normal);
			}
		}
#endif
	}
}
