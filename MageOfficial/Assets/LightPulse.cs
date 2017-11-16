using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightPulse : MonoBehaviour {

  //  public GameObject hole;
 //   public GameObject background;

    public Image hole;
    public Image background;

     Animator pulse;
    public ParticleSystem staff;
    

    
    // Use this for initialization
    void Start () {
        staff.Stop();
      pulse =  this.transform.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
        

    }
   public void Darkness()
    {
        hole.color = Color.black;
        background.color = Color.black;
        pulse.SetBool("pulse", false);
        staff.Stop();

    }

   public void Light()
    {
        Debug.Log("light");
        staff.Play();
        //uses Int32, must ? be hard coded in
        pulse.SetBool("pulse", true);
        hole.color  = new Color32(255, 0, 237, 50);
        background.color = new Color32(0, 0, 0, 180);
    }
}
