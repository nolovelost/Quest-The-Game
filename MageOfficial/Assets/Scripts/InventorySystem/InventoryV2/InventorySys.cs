using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySys : MonoBehaviour
{
    public GUISkin guiskin;

    private bool IsInventoryOn = false;

    private bool IsSlotDragging = false;
    private bool IsSlotOpened = false;
    float SlotLerpedPosition;

    float startVal = Screen.height; // Initial value from which to start
    float toVal = Screen.height - 68; // Value to reach.
    // starting value for the Lerp
    float t = 0.0f;

    public Potion mPotion;

    public float WidthOffset;
    public float HeightOffset;

    private bool ToggleInventory;

    public RadialPlayerHealth HealthScript;
    public RadialPlayerHealth ManaScript;

    // Use this for initialization
    void Start ()
    {
        float SlotLerpedPosition = startVal;
    }
	
	// Update is called once per frame
	void Update ()
    {
        ToggleInventory = true;
    }

    void OnGUI()
    {
        GUI.skin = guiskin;

        print(IsSlotOpened);

        if (ToggleInventory)
            DrawInventory();

        // GUI.Label(new Rect(10, tempScroll.ScrollID * 20, 200, 50), tempScroll.ScrollName);
    }

    void DrawInventory()
    {
        Event e = Event.current;

        Rect SlotBox = new Rect((Screen.width / 2 - 128) + WidthOffset, Screen.height - 68 + HeightOffset, 64, 64);
        Rect SlotPuller = new Rect((Screen.width / 2 - 128 + 26) + WidthOffset, Screen.height - 80 + HeightOffset, 124, 100);

        // Check for Slot Drag
        if (SlotPuller.Contains(e.mousePosition))
        {
            if (e.button == 0 && e.type == EventType.MouseDown)
            {
                if (IsSlotDragging == false && IsSlotOpened == false)
                {
                    IsSlotDragging = true;
                }
                if (IsSlotOpened == true)
                {
                    IsSlotDragging = false;
                    IsSlotOpened = false;
                    SlotLerpedPosition = startVal;
                    t = 0.0f;
                }
            }
        }

        if (IsSlotDragging == true)
        {
            DrawPotionSlot();
        }

        GUI.Box(
            SlotPuller,
            "",
            guiskin.GetStyle("GUI Slot Puller"));
    }

    void DrawPotionSlot()
    {
        Event e = Event.current;

        float SlotLerpedPosition = Mathf.Lerp(startVal, toVal, t);

        if (SlotLerpedPosition > toVal)
        {
            //Vector3 YPosition = new Vector3();
            //YPosition.y = Mathf.Clamp(Input.mousePosition.y, Screen.height - 68, Screen.height);

            Rect SlotBox = new Rect(
                (Screen.width / 2 - 128) + WidthOffset,
                SlotLerpedPosition,
                64,
                64);

            // .. and increase the t interpolater
            t += 4.0f * Time.deltaTime * Input.GetAxis("Mouse Y");

            GUI.Box(
                SlotBox,
                "",
                guiskin.GetStyle("GUI Slot"));

            //GUI.DrawTexture(SlotBox, mPotion.PotionIcon);

            //IsSlotOpened[i] = false;
        }

        if (SlotLerpedPosition <= toVal)
        {
            Rect SlotBox = new Rect(
                (Screen.width / 2 - 128) + WidthOffset,
                toVal,
                64,
                64);
            GUI.Box(
                SlotBox,
                "",
                guiskin.GetStyle("GUI Slot"));
            //GUI.DrawTexture(SlotBox, mPotion.PotionIcon);
            IsSlotOpened = true;

            if (SlotBox.Contains(e.mousePosition) && e.type == EventType.MouseDown)
            {
                UsePotion();
            }
        }
    }

    void UsePotion()
    {
        if (mPotion.TotalAmount > 0)
        {
            DoPotionEffect();

            mPotion.TotalAmount--;
            if (mPotion.TotalAmount <= 0)
            {
                DestroyObject(this.gameObject, 0.2f);
            }
        }
    }

    void DoPotionEffect()
    {
        // PUT POTION USE LOGIC HERE
        // ...

        if (mPotion.PotionEffect == Potion.Element.Health)
        {
            HealthScript.currentHealth += (int)mPotion.PotionPower;
            if (HealthScript.currentHealth > 100)
                HealthScript.currentHealth = 100;
        }
        if (mPotion.PotionEffect == Potion.Element.Mana)
        {
            ManaScript.currentHealth += (int)mPotion.PotionPower;
            if (ManaScript.currentHealth > 100)
                ManaScript.currentHealth = 100;
        }
    }

}