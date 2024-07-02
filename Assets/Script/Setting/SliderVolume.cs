using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SensitivitySlider;
    public void SetBGMVolume(float volume)
    {
        if (SoundManager.instance == null) return;
        SoundManager.instance.SetBgmVolume(volume);
    }
    public void SetSensitivity(float sensitivity)
    {
        if (SensitivityManager.instance == null) return;
        SensitivityManager.instance.SetSensitivity(sensitivity);
    }
    private void Start()
    {
        if (SoundManager.instance != null && BGMSlider != null)
            BGMSlider.value = SoundManager.instance.GetBgmVolume();
        if (SensitivityManager.instance != null && SensitivitySlider != null)
            SensitivitySlider.value = SensitivityManager.instance.GetSensitivity();

    }
}
