using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBreathManager : MonoBehaviour
{
    public static DynamicBreathManager instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] float breathSpeed, desiredBreathSpeed, breathLerpSpeed, breathVariance, returnToNormSpeed, returnCount, returnCountMax; // how fast are we breathing? this variable is set by MoodChange()
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

    public BreathMood breathMood; // our current breathmood
    BreathMood currentMood; // our mood on the last frame
    private void Start()
    {
        MoodChange(BreathMood.normal);
        StartCoroutine(BreathingCycle());
    }

    // call when we change our mood
    public void MoodChange(BreathMood desiredMood)
    {
        currentMood = desiredMood;
        breathMood = desiredMood;
        desiredBreathSpeed = breathSpeeds[(int)breathMood];
        returnCount = returnCountMax;
    }

    private void FixedUpdate()
    {
        // make sure we're on the right mood
        if (breathMood != currentMood) MoodChange(breathMood);

        // check to see if we should go back to our normal mood
        if (returnCount > 0)
        returnCount -= returnToNormSpeed * Time.fixedDeltaTime;

        if (returnCount <= 0)
        {
            MoodChange(BreathMood.normal);
        }

        // lerp our breathspeed to our desired breath speed
        breathSpeed = Mathf.Lerp(desiredBreathSpeed, breathSpeed, breathLerpSpeed * Time.fixedDeltaTime);
    }

    // coroutine to run our breathing cycle
    IEnumerator BreathingCycle()
    {
        // calculate and wait our breathspeed
        yield return new WaitForSecondsRealtime((breathSpeed / 1) + Random.Range(-breathVariance, breathVariance));
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
            lastBreath = breathInSounds[Random.Range(0, breathInSounds.Count)]; // pick a random sound...

        if (!lastBreathIn) // if our last breath was out...
            lastBreath = breathOutSounds[Random.Range(0, breathOutSounds.Count)]; // pick a random sound...

        // assign audio clip and play the sound
        breathSource.clip = lastBreath;
        breathSource.Play();
    }
}
