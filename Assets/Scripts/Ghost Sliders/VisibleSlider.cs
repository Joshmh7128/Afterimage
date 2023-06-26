using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleSlider : MonoBehaviour
{
    [SerializeField] float watchTime; // how long can we be seen for before moving?
    [SerializeField] float speed; // how fast do we move
    RaycastHit hit; bool seen; // have we been hit? have we been seen?
    [SerializeField] Vector3 targetPos; // the position we want to move to
    PlayerCameraController camController; // our camera controller
    [SerializeField] Renderer rend; // our renderer
    [SerializeField] AudioSource audioSource; // our audio source

    private void Start()
    {
        camController = PlayerCameraController.instance;
        if (rend == null)
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check for being seen
        CheckSeen();
        // apply our movement
        ApplyMovement();
    }

    void CheckSeen()
    {
        // check to see if this entity can see us
        if (!seen && rend.isVisible)
        {
            if (!Physics.Linecast(transform.position, camController.transform.position, out hit, Physics.AllLayers, QueryTriggerInteraction.Collide))
            {
                seen = true;
                // when we are seen apply faster breathing
                DynamicBreathManager.instance.MoodChange(DynamicBreathManager.BreathMood.scared);
                // when we are seen play our audio
                if (audioSource)
                    if (!audioSource.isPlaying)
                        audioSource.Play();
            }
        }
    }

    void ApplyMovement()
    {
        if (seen)
        watchTime -= Time.fixedDeltaTime;
        // only apply movement if we have been seen 
        if (seen && watchTime < 0)
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);
    }

}
