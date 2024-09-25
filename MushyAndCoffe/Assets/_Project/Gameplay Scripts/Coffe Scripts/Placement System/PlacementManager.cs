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
        private Quaternion objectRotation = new Quaternion();
        
        [SerializeField] private SerializedDictionary<Vector3Int, GameObject> cellInformation = new SerializedDictionary<Vector3Int, GameObject>();
        
#region Events      
        EventBinding<SelectFurnitureEvent> selectFurnitureEvent;
#endregion
        
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
        
        private void PlaceObject() 
        {
            bool hit = physics.MouseRaycastFromCamera(Input.mousePosition, usedCamera, 100f, placementLayerMask, out Vector3 clickLocation);
            
            if (!hit) return;
            
            var cell = grid.WorldToCell(clickLocation);
            var furnitureSO = (FurnitureSO) selectedObject.GetComponent<Furniture>().ScriptableObject;
            
            if (!CheckIfPlacementAllowed(furnitureSO, cell)) return;
            
            Debug.Log($"{clickLocation} | {cell}");
            
            var furnitureCreated = Instantiate(selectedObject, grid.GetCellCenterWorld(cell), objectRotation);
            
            OccupyCells(GetOccupiedCells(furnitureSO, cell), furnitureCreated);
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
                if (cellSelector != null) Destroy(cellSelector);
                return;	
            }
            
            if (previewObject == null) previewObject = Instantiate(selectedObject, clickLocation, new Quaternion());

            if (cellSelector == null) cellSelector = Instantiate(cellSelected, clickLocation, new Quaternion());

            var cell = grid.WorldToCell(clickLocation);
            
            Debug.Log($"{grid.GetCellCenterWorld(cell)}");
            
            previewObject.transform.position = grid.GetCellCenterWorld(cell);
            previewObject.transform.rotation = objectRotation;

            cellSelector.transform.position = grid.GetCellCenterWorld(cell);
        }
        
        private void ChangeSelectedObject(SelectFurnitureEvent selectFurnitureEvent) 
        {
            selectedObject = selectFurnitureEvent.selectedFurniture;
        }
        
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
