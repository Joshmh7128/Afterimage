using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement_Activator : PuzzleElement
{
    // what objects can we target?
    [SerializeField] GameObject tObj;
    [SerializeField] Renderer tRend;
    [SerializeField] Light tLight;

    // when we are activated, set our state
    internal override void Activate(States signal)
    {
        if (signal == States.on) try
            {
                tObj.SetActive(true);
                tRend.enabled = true;
                tLight.enabled = true;
            }
            catch { }
        else if (signal == States.off) try
            {
                tObj.SetActive(false);
                tRend.enabled = false;
                tLight.enabled = false;
            }
            catch { }

    }
}
