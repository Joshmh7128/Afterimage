using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleElement_Transform : PuzzleElement
{
    // our desired positions
    [SerializeField] Vector3 startPos, endPos;
    [SerializeField] Vector3 startRot, endRot;
    [SerializeField] Vector3 targetRot, targetPos;
    [SerializeField] float lerpSpeed; // how fast do we move?
    [SerializeField] States defaultState;

    private void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
        Activate(defaultState);
    }

    // what happens when we activate?
    internal override void Activate(States signal)
    {
        if (signal == States.on)
        {
            targetPos = endPos;
            targetRot = endRot;
        } else if (signal == States.off)
        {
            targetPos = startPos;
            targetRot = startRot;
        }
    }

    private void FixedUpdate()
    {
        ProcessTransform();
    }

    void ProcessTransform()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, lerpSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Slerp(Quaternion.Euler(transform.localEulerAngles), Quaternion.Euler(targetRot), lerpSpeed * Time.fixedDeltaTime);
    }

}
