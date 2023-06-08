using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleElement : MonoBehaviour
{
    // this class is a puzzle element class. all puzzle elements derrive from this
    // interact is triggered when the player looks at a puzzle element and left clicks, this is done in PlayerCameraController.cs
    internal virtual void Interact() { } // what happens when you interact with this, needs to be implemented on all puzzle elements
    // activate is triggered when another puzzle element is interacted with
    internal virtual void Activate() { } // does this puzzle element have an activated state?
    internal virtual void Activate(States signal) { } // does this puzzle element have an activated state?

    // all of the possible puzzle element states we can be in
    public enum States
    {
        off, on
    }

    public States state = States.off;
}
