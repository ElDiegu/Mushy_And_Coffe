using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using MushyAndCoffe.CraftingSystem;
using MushyAndCoffe.Managers;
using MushyAndCoffe.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace MushyAndCoffe.Databases
{
    public static class IngredientDatabase
    {
        public static List<IngredientSO> Ingredients { get; set; } = Resources.LoadAll<IngredientSO>("Scriptable Objects/Ingredients").ToList();
        
        public static IngredientSO FindByName(string name) 
        {
            return Ingredients.Where(ingredient => ingredient.IngredientName == name).FirstOrDefault();
        }
        
        public static IngredientSO FindByID(int ID) 
        {
            return Ingredients.Where(ingredient => ingredient.IngredientID == ID).FirstOrDefault();
        }
    }
}