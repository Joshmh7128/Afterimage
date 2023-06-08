using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement_Activator : PuzzleElement
{
    // what objects can we target?
    [SerializeField] List<GameObject> tObj;
    [SerializeField] bool localToggle; 

    // when we are activated, set our state
    internal override void Activate(States signal)
    {
        if (!localToggle)
        {
            if (signal == States.on) try
                {
                    foreach (GameObject go in tObj)
                        go.SetActive(true);
                }
                catch { }
            else if (signal == States.off) try
                {
                    foreach (GameObject go in tObj)
                        go.SetActive(false);
                }
                catch { }
        }

        if (localToggle)
            Activate();
    }

    internal override void Activate()
    {
        foreach (GameObject go in tObj)
            go.SetActive(!go.activeSelf);
    }
}
