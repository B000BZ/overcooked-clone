using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler onChangedState;
    public static GameManager Instance { get; private set; }

    enum State{
        isWaitingToStart,
        isCountdown,
        isPlaying,
        isGameOver
    }

    private State state;
    private float WaitingTimer = 1f;
    private float CountdownTimer = 3f;
    private float PlayingTimer = 30f;

    private void Awake()
    {
        state = State.isWaitingToStart;
        Instance = this;
    }

    private void Update()
    {
        switch (state) {
            case State.isWaitingToStart:
                WaitingTimer -= Time.deltaTime;

                if(WaitingTimer <= 0f)
                {
                    state = State.isCountdown;
                    onChangedState?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.isCountdown:
                CountdownTimer -= Time.deltaTime;

                if (CountdownTimer <= 0f)
                {
                    state = State.isPlaying;
                    onChangedState?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.isPlaying:
                PlayingTimer -= Time.deltaTime;

                if (PlayingTimer <= 0f)
                {
                    state = State.isGameOver;
                    onChangedState?.Invoke(this, EventArgs.Empty);

                }
                break;
            case State.isGameOver:
                break;
        }


        Debug.Log(state);

    }

    public bool IsGamePlaying()
    {
        return state == State.isPlaying;
    }

    public bool IsCountdown()
    {
        return state == State.isCountdown;
    }

    public float GetCountdownTimer()
    {
        return CountdownTimer;
    }

    public bool IsGameOver()
    {
        return state == State.isGameOver;
    }
}
