using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : MonoBehaviour
{

    public AudioClip sound1;
    AudioSource audioSource;

    void Start()
    {
        //Component‚ðŽæ“¾
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
    }


}
