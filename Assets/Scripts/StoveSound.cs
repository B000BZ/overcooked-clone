using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void Start()
    {
        stoveCounter.OnChangedState += StoveCounter_OnChangedState;
    }

    private void StoveCounter_OnChangedState(object sender, StoveCounter.OnChangedStateEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.frying || e.state == StoveCounter.State.fried;

        if (playSound)
        {
            audioSource.Play();

        }
        else
        {
            audioSource.Pause();
        }
    }
}
