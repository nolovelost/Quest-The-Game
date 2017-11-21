using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TimerBehaviour : MonoBehaviour
{
    private float TotalTime = 3.0f;
    private float CurrentTime;
    public float StartingTimer = 100.0f;                            // The amount of Time the player starts with
    public float CurrentTimer;                                      // The current Time the player has.
    public Slider TimeSlider;                                       // Reference to the UI's Time bar.
    public Slider TimeSlider2;
    public Image TimerEndingImage;                                  // Reference to an image to flash on the screen on being hurt.
    public float flashSpeed = 5f;                                   // The speed the TimerEndingImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);         // The colour the TimerEndingImage is set to, to flash.

    public float castTime = 3.0f;

    bool IsTiming = false;
    float NormalizedTime;

    // Use this for initialization
    void Start()
    {
        CurrentTimer = StartingTimer;
        CurrentTime = TotalTime;
    }

    // Update is called once per frame
    void Update()
    {
        TimeSlider.value = CurrentTimer;
        TimeSlider2.value = CurrentTimer;

        if (IsTiming == true)
        {
            CurrentTime = CurrentTime - Time.deltaTime;
            NormalizedTime = CurrentTime / TotalTime;
            CurrentTimer = NormalizedTime * 100.0f;
        }


        if (CurrentTime <= 0.0f)
        {
            ResetTimer();
        }
    }

    public void SetTimer(float Seconds)
    {
        TotalTime = Seconds;
    }

    public void StartTimer()
    {
        IsTiming = true;
    }

    public void ResetTimer()
    {
        CurrentTime = TotalTime;
        IsTiming = false;
    }

    public void StartCast()
    {
        ResetTimer();
        SetTimer(castTime);
            StartTimer();

    }
}
