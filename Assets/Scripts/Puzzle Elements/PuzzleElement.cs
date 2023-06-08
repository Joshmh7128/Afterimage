using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleElement : MonoBehaviour
{
    // this class is a puzzle element class. all puzzle elements derrive from this
    internal virtual void Interact() { } // what happens when you interact with this, needs to be implemented on all puzzle elements
    internal virtual void Activate() { } // does this puzzle element have an activated state?
    internal virtual void Activate(States signal) { } // does this puzzle element have an activated state?

    // all of the possible puzzle element states we can be in
    public enum States
    {
        off, on
    }

    public States state = States.off;

    // our code for checking if the mouse is over the object, and then pressing it
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // interact if we push the mouse button while looking directly at an object
            Interact();
        }
    }

}
