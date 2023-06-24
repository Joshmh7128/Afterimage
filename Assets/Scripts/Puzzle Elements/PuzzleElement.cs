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
    internal virtual void OnHover() { isHovering = true; hoverTime = 1f; }

    public bool isHovering; // is the player hovering on us right now?
    public float hoverTime; // our hovertime

    [SerializeField] Renderer FullHighlight; // our full highlight interaction render

    // all of the possible puzzle element states we can be in
    public enum States
    {
        off, on
    }

    public States state = States.off;

    public virtual void FixedUpdate()
    {
        ProcessHover();
    }

    void ProcessHover()
    {
        // our hover time
        if (hoverTime > 0) hoverTime -= Time.fixedDeltaTime*2;
        // if we lose hover
        if (hoverTime <= 0) isHovering = false;
        // if we are hovering turn on the full highlight
        if (FullHighlight)
        {
            FullHighlight.enabled = isHovering;
        }
    }
}
