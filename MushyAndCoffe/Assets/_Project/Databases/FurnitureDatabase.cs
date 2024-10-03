using System.Collections.Generic;
using System.Linq;
using MushyAndCoffe.ScriptableObjects;
using UnityEngine;

namespace MushyAndCoffe.Databases
{
	public static class FurnitureDatabase
	{
		public static List<FurnitureSO> Furnitures { get; private set; } = Resources.LoadAll<FurnitureSO>("Scriptable Objects/Furniture").ToList();
		
		public static FurnitureSO FindByID(int ID) 
		{
			return Furnitures.FirstOrDefault(furniture => furniture.ID == ID);
		} 
		
		public static FurnitureSO FindByName(string name) 
		{
			return Furnitures.FirstOrDefault(furniture => furniture.Name == name); 
		}
	}
}
