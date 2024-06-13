using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dead : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource;

    WaitForSeconds wait;

    void Start()
    {
        //Component‚ðŽæ“¾
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);

        wait = new WaitForSeconds(1.0f);

        StartCoroutine(nameof(DeadSound));

    }


    IEnumerator DeadSound()
    {
        yield return wait;
        audioSource.PlayOneShot(sound1);
    }
}
