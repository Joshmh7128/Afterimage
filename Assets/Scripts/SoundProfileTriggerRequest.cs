using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProfileTriggerRequest : MonoBehaviour
{
    public PlayerSoundProfile profile;

    private void OnTriggerEnter(Collider other)
    {
        // if we collide with the player, perform our change
        if (other.transform.CompareTag("Player"))
        {
            PlayerSoundController.instance.AudioMoodRequest(profile);
        }
    }
}
