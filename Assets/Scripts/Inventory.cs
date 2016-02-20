using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public int slotsX, slotsY;

	public bool showInventory;

	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();
	private ItemDatabase database;

	private bool showTooltip;
	private string toolTip;
	private bool draggingItem;
	private Item draggedItem;
	private int previousIndex;

	private bool inventoryOpen;

	// GUI's
	public GUISkin guiSkin;

	// Use this for initialization
	void Start () {

		for(int i = 0; i < (slotsX * slotsY); i++)
		{
			slots.Add(new Item());
			inventory.Add(new Item());
		}

		database = GameObject.FindGameObjectWithTag("InventoryDatabase").GetComponent<ItemDatabase>();

		AddItem(0);
	}


	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.I))
		{
			// Turn on/off task list
			showInventory = !showInventory;
		}

	}

	void OnGUI()
	{
		toolTip = "";
		GUI.skin = guiSkin;

		if(showInventory)
		{
			DrawInventory();
			if(showTooltip)
			{
				GUI.Box(new Rect(Event.current.mousePosition.x + 20, Event.current.mousePosition.y + 20, 200, 200), toolTip, guiSkin.GetStyle("Tooltip"));
			}
		}
		if(draggingItem)
		{
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
		}

	}

	void DrawInventory()
	{
		GUI.Box(new Rect(Screen.width / 2 - 205, Screen.height / 2 - 185, 300, 300), "Inventory", guiSkin.GetStyle("InventoryBG"));
		Event e = Event.current;
		int i = 0;
			for(int y = 0; y < slotsY; y++)
			{
				for(int x = 0; x < slotsX; x++)
				{
						Rect slotsrect = new Rect(x * 60 + Screen.width / 2 - 120, y * 60 + Screen.height / 2 - 120, 50, 50);
						GUI.Box(slotsrect, "", guiSkin.GetStyle("Slots"));

						slots[i] = inventory[i];

						if(slots[i].itemName != null)
						{
							GUI.DrawTexture(slotsrect, slots[i].itemIcon);
							if(slotsrect.Contains(Event.current.mousePosition))
							{
								CreateTooltip(slots[i]);
								showTooltip = true;

								if(e.isMouse && e.type == EventType.mouseDown && e.button == 1)
								{
									print("clicked " + i);
								}

								if(e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
								{
									draggingItem = true;
									previousIndex = i;
									draggedItem = slots[i];
									inventory[i] = new Item();
								}

								if(e.type == EventType.mouseUp && draggingItem)
								{
									inventory[previousIndex] = inventory[i];
									inventory[i] = draggedItem;
									draggingItem = false;
									draggedItem = null;
								}
							}
						} else {
							if(slotsrect.Contains(Event.current.mousePosition))
							{
								if(e.type == EventType.mouseUp && draggingItem)
								{
									inventory[i] = draggedItem;
									draggingItem = false;
									draggedItem = null;
								}
							}
						}

						if(toolTip == "")
						{
							showTooltip = false;
						}

						i++;
					}
			}

	}

	string CreateTooltip(Item item)
	{
		toolTip = "<color=#ffffff>" + item.itemName + "</color>\n\n" + item.itemDesc;
		return toolTip;
	}

	private void UseItem(Item item, int slot, bool deleteItem)
	{
		switch(item.itemID)
		{
			case 0:
				
			break;
		}

		if(deleteItem)
		{
			inventory[slot] = new Item();
		}
	}

	public void AddItem(int id)
	{
		for(int i = 0; i < inventory.Count; i++)
		{
			if(inventory[i].itemName == null)
			{
				for(int j = 0; j < database.items.Count; j++)
				{
					if(database.items[j].itemID == id)
					{
						inventory[i] = database.items[j];
						//Debug.Log("Added Item");
					}
				}
				break;
			}
		}
	}

	public void RemoveItem(int id)
	{
		for(int i = 0; i < inventory.Count; i++)
		{
			if(inventory[i].itemID == id)
			{
				inventory[i] = new Item();
				break;
			}
		}
	}

	public bool InventoryContains(int id)
	{
		bool result = false;
		for(int i = 0; i < inventory.Count; i++)
		{
			result = inventory[i].itemID == id;
			if(result)
			{
				break;
			}
		}
		return result;
	}

	/// <summary>
	/// Checks to see whether the item is containted within the inventory by using a string comparision
	/// instead of an intager.
	/// </summary>
	/// <returns><c>true</c>, if contain string was inventoryed, <c>false</c> otherwise.</returns>
	/// <param name="id">Identifier.</param>
    public bool InventoryContainString(string id)
    {
        bool result = false;
        for(int i = 0; i < inventory.Count; i++)
        {
            result = inventory[i].itemName == id;
            if(result)
            {
                break;
            }
        }
        return result;
    }
		                                
}
