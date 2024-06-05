using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void ChangeVolume(float volume)
    {
        gameObject.GetComponent<AudioSource>().volume = volume;
    }
}
