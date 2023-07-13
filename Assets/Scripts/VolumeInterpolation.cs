using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeInterpolation : MonoBehaviour
{
    Volume thisVol; // our volume
    [SerializeField] bool hasPlayer; // do we have the player?
    [SerializeField] float lerpSpeed = 0.05f;

    private void Start()
    {
        thisVol = GetComponent<Volume>();
    }

    private void FixedUpdate()
    {
        if (hasPlayer)
        {
            if (thisVol.weight < 1)
                thisVol.weight += Time.deltaTime * lerpSpeed;
        }
        else
        {
            if (thisVol.weight > 0)
                thisVol.weight -= Time.deltaTime * lerpSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hasPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hasPlayer = false;
        }
    }
}
