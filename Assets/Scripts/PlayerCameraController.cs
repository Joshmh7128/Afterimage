using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    // move to the player's head slowly
    [SerializeField] float lerpSpeed, slerpSpeed; // our movement speed params
    [SerializeField] Transform head; // the head we move to
    [SerializeField] float normalFov, zoomFov, fovLerpInSpeed, fovLerpOutSpeed;
    Camera cam; RaycastHit hit;

    public static PlayerCameraController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        TransformUpdate();
        ProcessFov();
        ProcessPuzzleInput();
    }

    void TransformUpdate()
    {
        // set position
        transform.position = Vector3.Lerp(transform.position, head.position, lerpSpeed * Time.fixedDeltaTime);
        // set rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, head.rotation, slerpSpeed * Time.fixedDeltaTime);
    }

    void ProcessFov()
    {
        if (Input.GetMouseButton(1))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomFov, fovLerpInSpeed * Time.fixedDeltaTime);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFov, fovLerpOutSpeed * Time.fixedDeltaTime);
        }
    }

    void ProcessPuzzleInput()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f, Physics.AllLayers, QueryTriggerInteraction.Collide))
        {
            if (hit.transform.gameObject.GetComponent<PuzzleElement>() != null)
            {
                if (Input.GetMouseButtonDown(0))
                    hit.transform.gameObject.GetComponent<PuzzleElement>().Interact();
            }
        }
    }
}
