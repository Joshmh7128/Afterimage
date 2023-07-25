using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement_Sound : PuzzleElement
{
    // our audio source
    [SerializeField] AudioSource audioSource;

    // play our sounds
    internal override void Activate() => audioSource.Play();
    internal override void Activate(States signal) => Activate();
}
