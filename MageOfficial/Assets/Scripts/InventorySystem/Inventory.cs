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

    private bool            ToggleInventory = false;   // Inventory closed by default

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < (SlotX * SlotY); i++)
        {
            Slots.Add(new global::Scroll());
        }

        mScrollDatabase = GameObject.FindGameObjectWithTag("ScrollDB").GetComponent<ScrollDatabase>();

        mInventory.Add(mScrollDatabase.ScrollList[0]);
        mInventory.Add(mScrollDatabase.ScrollList[1]);

        Count = mInventory.Count;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("ToggleInventory"))
        {
            ToggleInventory = !ToggleInventory;
        }

        Count = mInventory.Count;
    }

    void OnGUI()
    {
        GUI.skin = guiskin;

        if (ToggleInventory)
            DrawInventory();

        // GUI.Label(new Rect(10, tempScroll.ScrollID * 20, 200, 50), tempScroll.ScrollName);
    }

    void DrawInventory()
    {
        for (int y = 0; y < SlotY; y++)
        {
            for (int x = 0; x < SlotX; x++)
            {
                GUI.Box(
                    new Rect(x * 130, y * 130, 128, 128),
                    (x + y) < Count ? mInventory[x + y].ScrollIcon : guiskin.GetStyle("GUI Slot").normal.background,    // bad logic
                    guiskin.GetStyle("GUI Slot"));
            }
        }
    }
}
