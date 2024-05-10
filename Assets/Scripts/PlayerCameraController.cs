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

    public AudioSource camZoomNoise, flashlightNoise, vhsStaticNoise; // our zoome noise source
    [SerializeField] AudioClip zoomIn, zoomOut, clickIn, clickOut, flashlightOn, flashlightOff; // our noises
    bool lastClicked; // did we last click?

    public Light flashlightObj;

    float originalVol; // what was our original volume?

    public static PlayerCameraController instance;

    private void Awake()
    {
        // stop the audio source from playing audio on the camera on start
        originalVol = camZoomNoise.volume;
        camZoomNoise.volume = 0;
        instance = this;
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
        // invoke the late start
        Invoke("LateStart",0.1f);
    }

    // late start runs 0.1 seconds after start
    void LateStart()
    {
        // do this in the late start so we dont have the audio source play on start
        camZoomNoise.volume = originalVol;
    }

    private void Update()
    {
        TransformUpdate();
        ProcessFov();
        ProcessFlashlightInput();
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
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, zoomFov, fovLerpInSpeed * Time.fixedDeltaTime);
        }
        else
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, normalFov, fovLerpOutSpeed * Time.fixedDeltaTime);
        }

        if (cam.fieldOfView == zoomFov && !lastClicked)
        {
            lastClicked = true;
            camZoomNoise.Stop();
            camZoomNoise.PlayOneShot(clickIn);
        }

        if (cam.fieldOfView == normalFov && !lastClicked)
        {
            lastClicked = true;
            camZoomNoise.Stop();
            camZoomNoise.PlayOneShot(clickOut);
        }

        if (Input.GetMouseButtonDown(1))
        {
            lastClicked = false;
            camZoomNoise.clip = zoomIn; 
            camZoomNoise.Play();
        }

        if (Input.GetMouseButtonUp(1))
        {
            lastClicked = false;
            camZoomNoise.clip = zoomOut; 
            camZoomNoise.Play();
        }
    }

    void ProcessFlashlightInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flashlightObj.enabled = (!flashlightObj.enabled);
            AudioClip clip = flashlightObj.enabled ? flashlightOn : flashlightOff;
            flashlightNoise.PlayOneShot(clip);
        }
    }
    
}
