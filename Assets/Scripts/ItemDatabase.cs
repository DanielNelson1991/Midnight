using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();

	void Start()
	{
		// Add the following items to the database. NOTE: This DOES NOT add them to the inventory
		items.Add(new Item("Notepad", 0, "My trusty notepad", Item.ItemType.Note));
		items.Add(new Item("Main Door Key", 1, "Key to the main door. This item is used to open the main door...wherever that may be...", Item.ItemType.Key));
        items.Add(new Item("GKey", 2, "Key to the main door. This item is used to open the main door...wherever that may be...", Item.ItemType.Key));
        items.Add(new Item("TKey", 3, "Key to the main door. This item is used to open the main door...wherever that may be...", Item.ItemType.Key));
    }
}
