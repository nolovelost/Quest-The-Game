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

    public Sprite heart3;
    public Sprite heart2;
    public Sprite heart1;

    public GameObject heart;

    SpriteRenderer heartState;
    Animator animationControl;
	// Use this for initialization
	void Start () {
        currenthealth = startHealth;
        InvokeRepeating("Grunt", .5f, Random.Range(1.5f,5));
        heartState = heart.GetComponent<SpriteRenderer>();
        animationControl = this.transform.GetComponent<Animator>();
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
        if (currenthealth <20)
        {
            heartState.sprite = heart2;
        }

        if (currenthealth <= 0)
        {
            animationControl.SetTrigger("Die");
            heart.active = false;
            Destroy(this.gameObject,0.8f);
        }
		
	}


    public void TakeDamage(int damage)
    {
        animationControl.SetTrigger("Damage");
        currenthealth -= damage;
        damageTaken.Play();
    }
}
