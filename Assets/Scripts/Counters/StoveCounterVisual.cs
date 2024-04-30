using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveParticles;
    [SerializeField] private GameObject stoveFire;

    private void Start()
    {
        stoveCounter.OnChangedState += StoveCounter_OnChangedState;
    }

    private void StoveCounter_OnChangedState(object sender, StoveCounter.OnChangedStateEventArgs e)
    {
        bool isSizzling = e.state == StoveCounter.State.frying || e.state == StoveCounter.State.fried;

        stoveParticles.SetActive(isSizzling);
        stoveFire.SetActive(isSizzling);
    }
}
