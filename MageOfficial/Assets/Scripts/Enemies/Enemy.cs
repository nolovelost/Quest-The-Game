using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int startHealth;
    public int currenthealth;

    public AudioSource damageTaken;
    public AudioSource Grunt1;
    public AudioSource Grunt2;
    public AudioSource Grunt3;
	// Use this for initialization
	void Start () {
        currenthealth = startHealth;
        InvokeRepeating("Grunt", .5f, Random.Range(1.5f,5));
	}

    void Grunt()
    {
        
        AudioSource[] grunts = {Grunt1,Grunt2,Grunt3 };
        int pick = Random.Range(0, 3);
        grunts[pick].Play();
        
      //  damageTaken.Play();
    }
	// Update is called once per frame
	void Update () {

        if (currenthealth <= 0)
        {
            Destroy(this.gameObject);
        }
		
	}


    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        damageTaken.Play();
    }
}
