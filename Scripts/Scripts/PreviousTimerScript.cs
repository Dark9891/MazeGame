using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PreviousTimerScript : MonoBehaviour
{
    [SerializeField] TMP_Text prevTimeText;

    void Start()
    {
        float prevTime = PlayerPrefs.GetFloat("PreviousTime", 0f);
        int prevLength = PlayerPrefs.GetInt("PreviousLength", 0);
        int prevWidth = PlayerPrefs.GetInt("PreviousWidth", 0);

        TimeSpan time = TimeSpan.FromSeconds(prevTime);

        prevTimeText.text = "Previous time: " + time.ToString(@"mm\:ss") + " with size " + prevLength + " x " + prevWidth ;
    }
}
