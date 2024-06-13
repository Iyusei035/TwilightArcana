using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource;

    WaitForSeconds wait;

    void Start()
    {
        //Component‚ðŽæ“¾
        audioSource = GetComponent<AudioSource>();

        wait = new WaitForSeconds(1.8f);

        StartCoroutine(nameof(JumpSound));

    }


    IEnumerator JumpSound()
    {
        yield return wait;
        audioSource.PlayOneShot(sound1);
    }
}
