using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : ScriptableObject {

    public class InventoryClass
    {
        public GameObject Item;
        public string ItemName;
    }

    public static List<InventoryClass> InventoryItems = new List<InventoryClass>();

    public void AddItem(GameObject obj, string name)
    {
        InventoryItems.Add(new InventoryClass { Item = obj, ItemName = name });
        for(int i = 0; i < InventoryItems.Count; i++)
        {
            Debug.Log("Items In Inventory: " + InventoryItems[i].ItemName);
        }
    }

    public static void RemoveItem(string n)
    {
        InventoryItems.Remove(new InventoryClass() { ItemName = n });
        Destroy(GameObject.Find(n));
    }
}
