using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBreathManager : MonoBehaviour
{
    float breathSpeed, desiredBreathSpeed, breathLerpSpeed; // how fast are we breathing? this variable is set by MoodChange()
    [SerializeField] List<AudioClip> breathInSounds, breathOutSounds;
    AudioClip lastInSound, lastOutSound; // what were our last in and our breath sounds
    [SerializeField] AudioSource breathSource;
    [Header("Breathing in Breaths per Second")]
    [SerializeField] List<float> breathSpeeds;
    bool lastIn;
    [SerializeField] public enum BreathMood
    {
        // enum values are equal to breaths per second, multiplied by 0.1
        normal, scared, intense
    }

    public BreathMood breathMood  // our current breathmood
    {
        get { return breathMood; }
        set { breathMood = value; MoodChange(); } // when we change our mood, set the value, then run the function to check our mood
    }

    private void Start()
    {
        StartCoroutine(BreathingCycle());
    }

    // call when we change our mood
    void MoodChange()
    {
        desiredBreathSpeed = breathSpeeds[(int)breathMood];
    }

    private void FixedUpdate()
    {
        // lerp our breathspeed to our desired breath speed
        breathSpeed = Mathf.Lerp(desiredBreathSpeed, breathSpeed, breathLerpSpeed * Time.fixedDeltaTime);
    }

    // coroutine to run our breathing cycle
    IEnumerator BreathingCycle()
    {
        // calculate and wait our breathspeed
        yield return new WaitForSecondsRealtime(breathSpeed / 1);
        // what was our last breath?
        lastIn = !lastIn;
        // play a sound
        PlayBreath(lastIn);
        // continue
        StartCoroutine(BreathingCycle());
    }

    // play a breath out sound
    void PlayBreath(bool lastBreathIn)
    {
        // our vars
        AudioClip lastBreath = null;

        if (lastBreathIn) // if our last breath was in...
            while (lastBreath == lastInSound) // make sure we aren't using the last sound we made...
                lastBreath = breathInSounds[Random.Range(0, breathInSounds.Count)]; // pick a random sound...

        if (!lastBreathIn) // if our last breath was out...
            while (lastBreath == lastInSound) // make sure we aren't using the last sound we made...
                lastBreath = breathOutSounds[Random.Range(0, breathOutSounds.Count)]; // pick a random sound...

        // assign audio clip and play the sound
        breathSource.clip = lastBreath;
        breathSource.Play();
    }
}
