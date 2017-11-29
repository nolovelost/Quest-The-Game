using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public  GUISkin         guiskin;

    public  List<Scroll>    mInventory = new List<Scroll>();
    public  ScrollDatabase  mScrollDatabase;
    private int             Count;

    public  int             SlotX, SlotY;
    public  List<Scroll>    Slots = new List<Scroll>();

    private bool            ToggleInventory = false;
    private bool            ShowTooltip = false;
    private string          TooltipText;

    private bool            IsDraggingItem = false;
    private Scroll          DraggedItem;
    private int             PrevDraggedItemIndex;

    private bool[] IsSlotDragged = new bool[3];
    private bool[] IsSlotOpened = new bool[3];
    float SlotLerpedPosition;

    float startVal = Screen.height; // Initial value from which to start
    float toVal = Screen.height - 68; // Value to reach.
    // starting value for the Lerp
    float t = 0.0f;

    public bool HasUsedItem = false;


    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < (SlotX * SlotY); i++)
        {
            Slots.Add(new Scroll());
            mInventory.Add(new Scroll());
        }

        mScrollDatabase = GameObject.FindGameObjectWithTag("ScrollDB").GetComponent<ScrollDatabase>();

        // test add/remove
        AddScroll(2);
        AddScroll(1);
        AddScroll(1);
        RemoveScroll(1);
        AddScroll(2);

        Count = mInventory.Count;

        float SlotLerpedPosition = startVal;

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        ToggleInventory = true;
        //if (Input.GetButtonDown("ToggleInventory"))
        //{
        //ToggleInventory = !ToggleInventory;
        //}

        // Never show tooltip outside of inventory screen
        if (ToggleInventory == false)
            ShowTooltip = false;

        Count = mInventory.Count;
    }

    void OnGUI()
    {
        GUI.skin = guiskin;

        if (ToggleInventory)
            DrawInventory();

        if (ShowTooltip)
            DrawTooltip();

        if (IsDraggingItem)
            DrawItemDrag();

        // GUI.Label(new Rect(10, tempScroll.ScrollID * 20, 200, 50), tempScroll.ScrollName);
    }

    void DrawInventory()
    {
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < SlotY; y++)
        {
            for (int x = 0; x < SlotX; x++)
            {
                Rect SlotBox = new Rect((Screen.width/2 - 128) + (x*164), Screen.height - 68, 64, 64);
                Rect SlotPuller = new Rect((Screen.width/2 - 128 + 26) + (x * 164), Screen.height - 80, 124, 100);
                
                if (IsSlotDragged[i] == true)
                {
                    float SlotLerpedPosition = Mathf.Lerp(startVal, toVal, t);

                    if (SlotLerpedPosition > toVal)
                    {
                        //Vector3 YPosition = new Vector3();
                        //YPosition.y = Mathf.Clamp(Input.mousePosition.y, Screen.height - 68, Screen.height);

                        Rect SlotBoxes = new Rect(
                            (Screen.width / 2 - 128) + (x * 164),
                            SlotLerpedPosition,
                            64,
                            64);
                        
                        // .. and increate the t interpolater
                        t += 4.0f * Time.deltaTime * Input.GetAxis("Mouse Y");

                        GUI.Box(
                            SlotBoxes,
                            "",
                            guiskin.GetStyle("GUI Slot"));

                        //IsSlotOpened[i] = false;
                    }

                    if (SlotLerpedPosition <= toVal)
                    {
                        Rect SlotBoxes = new Rect(
                            (Screen.width / 2 - 128) + (x * 164),
                            toVal,
                            64,
                            64);
                        GUI.Box(
                            SlotBoxes,
                            "",
                            guiskin.GetStyle("GUI Slot"));
                        IsSlotOpened[i] = true;
                    }
                }
                
                GUI.Box(
                    SlotPuller,
                    "",
                    guiskin.GetStyle("GUI Slot Puller"));

                Slots[i] = mInventory[i];   // Add all inventory items to the slot

                // Check for Slot Drag
                if (SlotPuller.Contains(e.mousePosition))
                {
                    if (e.button == 0 && e.type == EventType.MouseDown)
                    {
                        //print(x);
                        if (IsSlotDragged[i] == false)
                        {
                            IsSlotDragged[i] = true;
                        }
                        else
                        {
                            for (int j = 0; j < SlotX; j++)
                            {
                                IsSlotDragged[j] = false;
                                IsSlotOpened[j] = false;
                            }                              
                            t = 0.0f;
                        }
                    }
                }

                if (IsSlotOpened[i] == true)
                {
                    // Swap item on Mouse Release logic
                    if (SlotBox.Contains(e.mousePosition) && e.type == EventType.MouseUp && IsDraggingItem)
                    {
                        mInventory[PrevDraggedItemIndex] = mInventory[i];
                        mInventory[i] = DraggedItem;
                        IsDraggingItem = false;
                        DraggedItem = null;
                    }

                    // Check to see if the slot is occupied by an inventory icon
                    if (Slots[i].ScrollID != 0)
                    {
                        GUI.DrawTexture(SlotBox, Slots[i].ScrollIcon);

                        if (SlotBox.Contains(e.mousePosition))
                        {
                            TooltipText = CreateTooltip(Slots[i]);
                            ShowTooltip = true;

                            // Dragging item on Mouse Drag logic
                            if (e.button == 0 && e.type == EventType.MouseDrag && !IsDraggingItem)
                            {
                                IsDraggingItem = true;
                                PrevDraggedItemIndex = i;
                                DraggedItem = Slots[i];
                                mInventory[i] = new Scroll();
                                ShowTooltip = false;
                            }
                        }
                        else
                            ShowTooltip = false;
                    }
                }

                i++;
            }
        }
    }

    // Text for the tooltip
    string CreateTooltip(Scroll scroll)
    {
        return scroll.ScrollName + "\n\n" + scroll.Description;
    }

    void DrawTooltip()
    {
        Rect TooltipRect = new Rect(Event.current.mousePosition.x + 10, Event.current.mousePosition.y - 64, 200, 200);
        GUI.Box(TooltipRect, TooltipText, guiskin.GetStyle("GUI Slot"));
    }

    bool AddScroll(int id)
    {
        for (int i = 0; i < mInventory.Count; i++)
        {
            // If the item already exists with an arbitrary amount above 0
            if (mInventory[i].ScrollID == id)
            {
                mInventory[i].TotalAmount++;
                return true;
            }
        }

        for (int i = 0; i < mInventory.Count; i++)
        {
            // If the item doesn't exist then add the new item
            if (mInventory[i].ScrollID == 0)
            {
                for (int j = 0; j < mScrollDatabase.ScrollList.Count; j++)
                {
                    if (mScrollDatabase.ScrollList[j].ScrollID == id)
                    {
                        mInventory[i] = mScrollDatabase.ScrollList[j];
                        return true;
                    }
                }
            }
        }
        return false;
    }

    bool InventoryContains(int id)
    {
        for (int i = 0; i < mInventory.Count; i++)
        {
            if (mInventory[i].ScrollID == id)
            {
                return true;
            }
        }
        return false;
    }

    void RemoveScroll(int id)
    {
        Scroll EmptyScroll = new Scroll();
        for (int i = 0; i < mInventory.Count; i++)
        {
            if (mInventory[i].ScrollID != 0)
            {
                if (mInventory[i].TotalAmount != 1)
                {
                    mInventory[i].TotalAmount--;
                    return;
                }
                else
                {
                    mInventory[i] = EmptyScroll;
                    return;
                }
            }
        }
    }

    void DrawItemDrag()
    {
        Rect DragBox = new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50);
        GUI.DrawTexture(DragBox, DraggedItem.ScrollIcon);
    }

    bool UseScroll(int i)
    {
        if (mInventory[i].ScrollID == 0)
        {
            return false;
        }
        if (mInventory[i].TotalAmount == 0)
        {
            return false;
        }

        // PUT USE LOGIC HERE

        mInventory[i].TotalAmount--;
        if (mInventory[i].TotalAmount == 0)
        {
            mInventory[i] = new Scroll();
            return true;
        }
        return true;
    }

    void DropScroll(int i)
    {
        // PUT DROP LOGIC HERE

        RemoveScroll(mInventory[i].ScrollID);
    }
}
