using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class RadialPlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;                          // The amount of health the player starts the game with.
    public int currentHealth;                                 // The current health the player has
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    bool damaged;                                               // True when the player gets damaged.
    private float SliderAmount;
    bool IsTakingDamage = false;
    bool HasTakenDamage = true;



    void Awake()
    {
        // Set the initial health of the player.
        currentHealth = startingHealth;
        SliderAmount = (float)currentHealth / 400;

        GetComponent<Image>().fillAmount = SliderAmount;
    }


    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // set slider normalized value
        SliderAmount = (float)currentHealth / 400;
        // Set the health bar's value to the current health.
        GetComponent<Image>().fillAmount = SliderAmount;
        //GetComponent<Image>().fillAmount = Mathf.Lerp(currentHealth, SliderAmount, 10 * Time.deltaTime);

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0)
        {
            // ... it should die.
            Death();
        }
    }


    void Death()
    {
        print("death.");
        // DestroyObject(**** get a reference to the player character to destroy ****);
    }
}