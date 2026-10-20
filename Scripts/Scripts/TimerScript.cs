using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{

    bool timerActive = false;
    float timerTime = 0;
    [SerializeField] TMP_Text timerText;
    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (timerActive)
        {
            timerTime = timerTime + Time.deltaTime;
        }
        

        TimeSpan time = TimeSpan.FromSeconds(timerTime);
        timerText.text = time.ToString(@"mm\:ss"); ;
    }

    public void StartTimer()
    {
        timerActive = true; 
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public void ToggleTimer()
    {
        timerActive = !timerActive;
    }

    public void ResetTimer()
    {
        timerTime = 0; 
    }

    public float ReturnTimeValue()
    { 
        return timerTime; 
    }
}
