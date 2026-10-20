using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class rowSlideController : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;

    private void Start()
    {
        slider.onValueChanged.AddListener(UpdateSliderText);
        UpdateSliderText(slider.value);
    }

    private void UpdateSliderText(float value)
    {
        sliderText.text = Mathf.RoundToInt(value).ToString();

        UIManager uiManager = GameObject.Find("ButtonController").GetComponent<UIManager>();
        uiManager.SetRowSliderValue(value);
    }
}
