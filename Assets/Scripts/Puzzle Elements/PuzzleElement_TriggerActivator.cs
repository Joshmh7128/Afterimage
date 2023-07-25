using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement_TriggerActivator : PuzzleElement
{
    /// activates other puzzle elements when the player touches its trigger
    /// 

    [SerializeField] bool activated;
    [SerializeField] List<PuzzleElement> elements;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !activated)
        {
            activated = true;

            foreach (PuzzleElement element in elements)
                element.Activate();
        }
    }
}
