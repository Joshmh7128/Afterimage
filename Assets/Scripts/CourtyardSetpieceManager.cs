using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtyardSetpieceManager : MonoBehaviour
{
    /// script exists to manage the courtyard setpiece
    /// 

    [SerializeField] AudioSource corruption;
    [SerializeField] Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!corruption.isPlaying) corruption.Play();
        }
    }

    void StartAnimation()
    {

    }

}
