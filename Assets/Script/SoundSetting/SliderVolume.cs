using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public void SetBGMVolume(float volume)
    {
        if (SoundManager.instance == null) return;
        SoundManager.instance.SetBgmVolume(volume);
    }

    private void Start()
    {
        if (SoundManager.instance == null) return;
        gameObject.GetComponent<Slider>().value = SoundManager.instance.GetBgmVolume();
    }
}
