using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioNaturalizer : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Naturalize();
    }

    void Naturalize()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
    }
}
