using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement_Button : PuzzleElement
{
    [SerializeField] List<PuzzleElement> activatablePuzzleElements;
    [SerializeField] float PressTime; // how long is this button pressed for?

    bool pressing; 

    // what happens when we interact?
    internal override void Interact()
    {
        if (!pressing)
        {
            Debug.Log("button pressed");
            StartCoroutine(Press());
        }
    }

    // our press coroutine
    IEnumerator Press()
    {
        pressing = true;

        // activate all target elements
        try
        {
            foreach (PuzzleElement element in activatablePuzzleElements)
                element.Activate(States.on);
        } catch { }

        yield return new WaitForSeconds(PressTime);

        // deactivate all target elements
        try
        {
            foreach (PuzzleElement element in activatablePuzzleElements)
                element.Activate(States.off);
        } catch { }

        pressing = false;
    }

}
