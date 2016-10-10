using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    private bool _isInventoryOpen;
    public CanvasGroup _CanvasObject;
    public GameObject emptySlot;

    private RectTransform inventoryRect;

    public int slots, rows;
    public float slotSize;

	// Use this for initialization
	void Start () {

        _isInventoryOpen = false;

	}
	
	// Update is called once per frame
	void Update () {
	    
        // If the player requests inventory, call function to open it.
        if(Input.GetKeyDown(KeyCode.I))
        {
            _isInventoryOpen = !_isInventoryOpen;
            DrawInventory();
        }

	}

    public void DrawInventory()
    {
        if(_isInventoryOpen)
        {
            _CanvasObject.gameObject.SetActive(true);
            inventoryRect = _CanvasObject.gameObject.GetComponent<RectTransform>();

            for(int x = 0; x < 4; x++)
            {
               for(int y = 0; y < 5; y++)
                {
                    Instantiate(emptySlot, inventoryRect.transform.position, inventoryRect.transform.rotation);
                }
                
            }
            Debug.Log("Inventory Rect is " + inventoryRect);
        } else
        {
            _CanvasObject.gameObject.SetActive(false);
        }
    }
}
