using MushyAndCoffe.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MushyAndCoffe.PlacementSystem
{
    public class InventorySlots : MonoBehaviour
    {
        private List<Furniture> furnitures;

        public GameObject itemSlot;


        private void Start()
        { 

            furnitures = InventoryManager.Instance.Furnitures;
            //Item slots
            foreach (Furniture furni in furnitures)
            {
                var slotObject = Instantiate(itemSlot, transform);
                slotObject.GetComponent<Slot>().Furni = furni;
                GameObject iconObject = slotObject.transform.GetChild(0).gameObject;
                iconObject.SetActive(true);
                iconObject.GetComponent<Image>().sprite = (furni.ScriptableObject as FurnitureSO).Icon;
            }

            //Empty slots
            int nEmptySlots = 20 - furnitures.Count;
            for (int i = 0; i < nEmptySlots; i++) Instantiate(itemSlot, transform);

        }


    }
}
