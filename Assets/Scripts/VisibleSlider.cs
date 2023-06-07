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
            Debug.Log("seeable");

            if (!Physics.Linecast(transform.position, camController.transform.position, out hit, Physics.AllLayers, QueryTriggerInteraction.Collide))
            {
                Debug.Log("seen");
                seen = true;
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
