using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls all of the sound emitters on the player
/// </summary>
public class PlayerSoundController : MonoBehaviour
{
    public static PlayerSoundController instance;
    private void Awake()
    {
        instance = this; 
    }

    [SerializeField] AudioSource staticSource;
    float staticSourceTargetVol;

    [SerializeField] FootstepProfile currentFootstepProfile;
    [SerializeField] float footstepInterval;
    [SerializeField] AudioSource footstepSource;
    [SerializeField] Vector3 lastStep; // where we took our last step

    // other objects call this to change elements of the sound profile
    public void AudioMoodRequest(PlayerSoundProfile profile)
    {
        // set the static level
        if (profile.staticVolume >= 0)
            staticSourceTargetVol = profile.staticVolume;
    }

    void ProcessAudioMood()
    {
        staticSource.volume = Mathf.Lerp(staticSource.volume, staticSourceTargetVol, Time.deltaTime);
    }

    // runs ever frame to process footsteps
    void ProcessFootsteps()
    {
        // if the distance from our last step to our current position is more than our interval, take a step
        if (Vector3.Distance(lastStep, PlayerController.instance.transform.position) > footstepInterval)
        {
            lastStep = PlayerController.instance.transform.position;
            footstepSource.clip = currentFootstepProfile.footsteps[Random.Range(0, currentFootstepProfile.footsteps.Count)];
            footstepSource.pitch = Random.Range(0.9f, 1.1f);
            footstepSource.Play();
        }
    }

    private void FixedUpdate()
    {
        // process the changes in audio each frame
        ProcessAudioMood();
        ProcessFootsteps();
    }
}
