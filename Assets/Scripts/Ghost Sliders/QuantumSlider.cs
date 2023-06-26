using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumSlider : MonoBehaviour
{
    bool seen;
    [SerializeField] Renderer rend, quantumBox;
    PlayerCameraController camController;
    RaycastHit hit;

    [SerializeField] float waitTime; // how long do we want before coming back

    // Start is called before the first frame update
    void Start()
    {
        camController = PlayerCameraController.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckSeen();
    }

    void CheckSeen()
    {
        // check to see if this entity can see us
        if (!seen && quantumBox.isVisible)
        {
            Debug.Log("quantum seeable");

            if (!Physics.Linecast(transform.position, camController.transform.position, out hit, Physics.AllLayers, QueryTriggerInteraction.Collide))
            {
                Debug.Log("quantum seen");
                seen = true;
            }
        }

        if (quantumBox.isVisible == false && seen == true)
        {
            Debug.Log("quantum unseen");
            StartCoroutine(ReloadTime());
        }
    }   

    IEnumerator ReloadTime()
    {
        rend.enabled = false;
        yield return new WaitForSecondsRealtime(waitTime);

        if (!quantumBox.isVisible)
        {
            rend.enabled = true;
            seen = false;
        }
        else StartCoroutine(ReloadTime());
    }

}
