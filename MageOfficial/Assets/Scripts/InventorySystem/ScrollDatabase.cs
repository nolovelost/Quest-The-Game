using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class ScrollDatabase : MonoBehaviour
{
    public List<Scroll> ScrollList = new List<Scroll>();

    void Start()
    {
        ScrollList.Add(new Scroll(
            "Fire", 
            1, 
            "Tower of fire and decay", 
            Scroll.Element.Fire, 
            100.0f, 
            5.0f));

        ScrollList.Add(new Scroll(
            "Current", 
            2, 
            "Tide of electricity, death's undertow", 
            Scroll.Element.Electricity, 
            80.0f, 
            4.45f));
    }

}
