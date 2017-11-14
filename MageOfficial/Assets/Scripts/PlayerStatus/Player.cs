using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject healthObj;
    public GameObject manaObj;
    public int startingHealth = 100;                          // The amount of health the player starts the game with.
    public int currentHealth;

   public RadialPlayerHealth health;
    RadialPlayerHealth mana;
    void Start()
    {
        currentHealth = startingHealth;
        health = healthObj.GetComponent<RadialPlayerHealth>();
        mana = manaObj.GetComponent<RadialPlayerHealth>();
    }

    void attackHit(int damage)
    {
        health.TakeDamage(damage);
    }

    void manaDeplete(int amount)
    {
        mana.TakeDamage(amount);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            attackHit(10);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            manaDeplete(10);
        }

    }
}
