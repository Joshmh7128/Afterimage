using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleLerp : MonoBehaviour
{
    // our two transforms
    [SerializeField] Transform startTransform, targetTransform, startRep;
    [SerializeField] Vector3 startPos; 
    [SerializeField] float lerpDistance; // the amount of % distance we want to lerp every frame
    [SerializeField] bool lerpTo;

    private void Start()
    {
        // set it once
        startPos = startTransform.position;
    }

    // we want to lerp our start to our target
    private void FixedUpdate()
    {
        if (lerpTo)
            lerpDistance = Mathf.Lerp(lerpDistance, 1, Time.fixedDeltaTime);
        else
            lerpDistance = Mathf.Lerp(lerpDistance, 0, Time.fixedDeltaTime);

        startPos = startRep.position;

        // do the lerp
        startTransform.position = Vector3.Lerp(startTransform.position, targetTransform.position, lerpDistance * Time.fixedDeltaTime);
    }
}
