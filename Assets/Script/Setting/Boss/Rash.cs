using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rash : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;

    WaitForSeconds wait;

    void Start()
    {
        //Component‚ðŽæ“¾
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(sound2);

        wait = new WaitForSeconds(1.9f);

        StartCoroutine(nameof(Rash_Exp));

    }


    IEnumerator Rash_Exp()
    {
        yield return wait;
        audioSource.Stop();
        audioSource.PlayOneShot(sound1);
    }
}
