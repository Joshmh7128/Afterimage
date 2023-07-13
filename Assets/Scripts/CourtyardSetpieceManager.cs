using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtyardSetpieceManager : MonoBehaviour
{
    /// script exists to manage the courtyard setpiece
    /// 

    [SerializeField] Animator animator;
    bool started;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AnimationCheck();
        }
    }

    void AnimationCheck()
    {
        if (!started && PlayerCameraController.instance.flashlightObj.isActiveAndEnabled)
        {
            started = true;
            animator.Play("Flicker");
        }
    }

}
