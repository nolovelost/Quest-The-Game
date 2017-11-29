using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDB : MonoBehaviour {

    public List<Potion> PotionList = new List<Potion>();

    void Start()
    {
        PotionList.Add(new Potion(
            "Heal",
            1,
            "Heals the mage",
            Potion.Element.Health,
            50.0f,
            2));

        PotionList.Add(new Potion(
            "Mana",
            1,
            "Mana is replenished for the mage",
            Potion.Element.Mana,
            30.0f,
            2));
    }
}
