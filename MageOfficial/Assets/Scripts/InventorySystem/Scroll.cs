using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class Scroll
{
    public enum Element
    {
        Fire = 0,
        Water,
        Electricity,
        Blood
    }

    public  string       ScrollName = "Unnamed_Scroll";
    public  int          ScrollID = 0;
    public  string       Description = "Absent_Inscription";
    public  Texture2D    ScrollIcon;
    public  Texture      ScrollImage;
    public  Texture      ScrollPageView;
    public  AudioSource  CastSound;
    public  Element      MagicElement;
    public  float        CastPower;
    public  float        CastDelay;
    public  int          TotalAmount;


    public Scroll() { }

    public Scroll(
            string  scrollName,
            int     scrollID,
            string  description,
            Element magicElement,
            float   castPower,
            float   castDelay,
            int     totalAmount = 1)
    {
        // Basic spell information
        ScrollName      =   scrollName;
        ScrollID        =   scrollID;
        Description     =   description;

        // Load resources
        ScrollIcon      =   Resources.Load<Texture2D>("spell icons/" + scrollName);
        ScrollImage     =   Resources.Load<Texture>("scroll image/" + scrollName);
        ScrollPageView  =   Resources.Load<Texture>("scroll pages/" + scrollName);
        CastSound       =   Resources.Load<AudioSource>("cast sounds/" + scrollName);

        // Gameplay specific information
        MagicElement    =   magicElement;
        CastPower       =   castPower;
        CastDelay       =   castDelay;
        TotalAmount     =   totalAmount;
    }
        
}
