using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Footstep Profile", menuName = "Custom Profiles/Footstep Profile")]
public class FootstepProfile : ScriptableObject
{
    public List<AudioClip> footsteps;
}
