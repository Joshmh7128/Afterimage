using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    Transform head;
    [SerializeField] float lerpSpeed;

    private void Start()
    {
        head = PlayerController.instance.playerHead;
    }

    private void FixedUpdate()
    {
        transform.position = head.transform.position;
        transform.rotation = Quaternion.Slerp(Quaternion.Euler(transform.eulerAngles), Quaternion.Euler(head.transform.eulerAngles), lerpSpeed * Time.fixedDeltaTime);
    }
}
