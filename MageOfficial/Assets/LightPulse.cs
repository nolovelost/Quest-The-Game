using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightPulse : MonoBehaviour {

  //  public GameObject hole;
 //   public GameObject background;

    public RectTransform hole;
    public RectTransform background;

    public float ler = 1.0f;
    public int fluct = 1;
    // Use this for initialization
    void Start () {
        InvokeRepeating("Smaller", 1, ler);
        InvokeRepeating("Bigger", 1.5f,ler);
    }
	
	// Update is called once per frame
	void Update () {
        //Smaller();
       // Bigger();
        

    }
    void Smaller()
    {

        hole.sizeDelta = Vector2.Lerp(new Vector2(hole.sizeDelta.x, hole.sizeDelta.y), new Vector2(hole.sizeDelta.x - fluct, hole.sizeDelta.y - fluct),Time.deltaTime);
        background.sizeDelta = Vector2.Lerp(new Vector2(background.sizeDelta.x, background.sizeDelta.y), new Vector2(background.sizeDelta.x - fluct, background.sizeDelta.y - fluct),  Time.deltaTime);
    }

    void Bigger()
    {
        hole.sizeDelta = Vector2.Lerp(new Vector2(hole.sizeDelta.x, hole.sizeDelta.y), new Vector2(hole.sizeDelta.x + fluct, hole.sizeDelta.y + fluct),  Time.deltaTime);
        background.sizeDelta = Vector2.Lerp(new Vector2(background.sizeDelta.x, background.sizeDelta.y), new Vector2(background.sizeDelta.x + fluct, background.sizeDelta.y + fluct),  Time.deltaTime);
    }
}
