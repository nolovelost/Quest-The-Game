using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public enum Element
    {
        Health = 0,
        Mana,
        Timer
    }

    public string PotionName = "Unnamed_Potion";
    public int PotionID = 0;
    public string Description = "Absent_Inscription";
    public Element PotionEffect;
    public Texture PotionIcon;
    public float PotionPower;
    public int TotalAmount;

    public Potion() { }

    public Potion(
            string potionName,
            int potionID,
            string description,
            Element potionEffect,
            float potionPower,
            int totalAmount = 1)
    {
        // Basic spell information
        PotionName = potionName;
        PotionID = potionID;
        Description = description;

        // Load resources
        PotionIcon = Resources.Load<Texture>("potion icons/" + PotionName);

        // Gameplay specific information
        PotionEffect = potionEffect;
        PotionPower = potionPower;
        TotalAmount = totalAmount;
    }
}
