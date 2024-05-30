using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class AudioClipsListSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliverySuccess;
    public AudioClip[] deliveryFail;
    public AudioClip[] itemDrop;
    public AudioClip[] itemPickup;
    public AudioClip[] footsteps;
    public AudioClip[] trash;
    public AudioClip[] warning;

}
