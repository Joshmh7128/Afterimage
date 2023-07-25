using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement_ToggleObject : PuzzleElement
{
    [SerializeField] bool invertSignal; // should this invert the signal?
    [SerializeField] bool usesTrigger; // does this use a trigger?

    // this puzzle element changes its active state when activated
    internal override void Activate(States state)
    {
        Debug.Log("running " + state.ToString());

        if (!invertSignal)
        {
            if (state == States.on)
                gameObject.SetActive(true);

            if (state == States.off)
                gameObject.SetActive(false);
        }
        
        if (invertSignal)
        {
            if (state == States.on)
                gameObject.SetActive(false);

            if (state == States.off)
                gameObject.SetActive(true);
        }
        
    }
}
