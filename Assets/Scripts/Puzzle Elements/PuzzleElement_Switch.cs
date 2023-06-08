using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement_Switch : PuzzleElement
{
    [SerializeField] List<PuzzleElement> activatablePuzzleElements;

    // when we interact with this switch, toggle it on and off
    internal override void Interact()
    {
        // change the state
        state = ToggleOnOff();
        // send the signal
        foreach (PuzzleElement element in activatablePuzzleElements)
        {
            // send the current signal
            element.Activate(state);
        }
    }

    // toggles the switch
    States ToggleOnOff()
    {
        if (state == States.off)
            return States.on;
        else if (state == States.on)
            return States.off;
        else return States.off;
    }
}
