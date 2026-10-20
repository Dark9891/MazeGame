using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider slider;
    public AudioSource musicAudioSource;
    public TextMeshProUGUI sliderText;

    void Start()
    {
        // Initialize slider value to current music volume
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);

        // Set the volume of the music audio source to match the initial slider value
        musicAudioSource.volume = slider.value;

        // Add listeners for the slider's value change event
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        slider.onValueChanged.AddListener(UpdateSliderText);

        // Update the slider text with the initial value
        UpdateSliderText(slider.value);
    }

    void OnSliderValueChanged()
    {
        // Set the volume of the music audio source
        musicAudioSource.volume = slider.value;

        // Save the current volume to PlayerPrefs for persistence
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
    }

    private void UpdateSliderText(float value)
    {
        // Update the text to display the current value of the slider
        sliderText.text = Mathf.RoundToInt(value * 100f).ToString() + "%";
    }
}
