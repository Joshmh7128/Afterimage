using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement_Page : PuzzleElement
{
    /// when a page is picked up play a sound and move it towards the player
    /// 

    AudioSource audioSource; // our audio source
    bool canMove, taken; // can we move?

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // when we activate, move towards the player
    internal override void Interact()
    {
        // increase the amount of photos that we have
        if (!taken)
        {
            taken = true;
            UIHandler.instance.photoCount++;
        }
        canMove = true;
        audioSource.Play(); // play audio
        // destroy the page once we have it
        Invoke("ManualDestroy", 1f);

    }

    // our fixed update
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, 1 * Time.fixedDeltaTime);
        }
    }

    // destroy ourselves
    void ManualDestroy()
    {
        Destroy(gameObject);
    }
}
