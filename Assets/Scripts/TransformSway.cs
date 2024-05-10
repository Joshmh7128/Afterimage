using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSway : MonoBehaviour
{
    // this script moves the transform around slowly in local space
    Vector3 targetPos, targetRot;
    [SerializeField] float range, maxTime, lerpSpeed;

    private void Start()
    {
        StartCoroutine(Counter());
    }

    IEnumerator Counter()
    {
        SetPos();
        yield return new WaitForSeconds(Random.Range(maxTime/2, maxTime));
        StartCoroutine(Counter());
    }

    void SetPos()
    {
        targetPos = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
        targetRot = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));

    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, lerpSpeed * Time.fixedDeltaTime);
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, transform.localEulerAngles+targetRot, lerpSpeed * Time.fixedDeltaTime);   
    }
}
