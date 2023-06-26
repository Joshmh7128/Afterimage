using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement_ToggleObject : PuzzleElement
{
    // this puzzle element changes its active state when activated
    internal override void Activate()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
