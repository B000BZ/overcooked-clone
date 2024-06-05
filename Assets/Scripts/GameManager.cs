using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler onChangedState;
    public event EventHandler onGamePaused;
    public event EventHandler onGameUnpaused;
    public event EventHandler onOptionsShown;
    public event EventHandler onOptionsNotShown;

    enum State {
        isWaitingToStart,
        isCountdown,
        isPlaying,
        isGameOverCountdown,
        isGameOver
    }

    private State state;
    private float waitingTimer = 0f;
    private float countdownTimer = 3f;
    private float gameOverCountdownTimer = 3f;
    private float playingTimer = 250f;
    public bool isPaused = false;
    private bool isOptions = false;

    private void Awake()
    {
        state = State.isWaitingToStart;
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnPaused += GameInput_OnPaused;
        GamePlayUi.Instance.OnMenuButtonClick += GamePlayUi_OnMenuButtonClick;
    }

    private void GamePlayUi_OnMenuButtonClick(object sender, EventArgs e)
    {
        TogglePause();
    }

    private void Update()
    {        

        switch (state) {
            case State.isWaitingToStart:
                waitingTimer -= Time.deltaTime;

                if (waitingTimer <= 0f)
                {
                    state = State.isCountdown;
                    onChangedState?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.isCountdown:
                countdownTimer -= Time.deltaTime;

                if (countdownTimer <= 0f)
                {
                    state = State.isPlaying;
                    onChangedState?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.isPlaying:
                playingTimer -= Time.deltaTime;

                if (playingTimer <= 0f)
                {
                    state = State.isGameOverCountdown;
                    onChangedState?.Invoke(this, EventArgs.Empty);

                }
                break;
            case State.isGameOverCountdown:
                gameOverCountdownTimer -= Time.deltaTime;

                if (gameOverCountdownTimer <= 0f)
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

    private void GameInput_OnPaused(object sender, EventArgs e)
    {
        TogglePause();
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
        return countdownTimer;
    }

    public bool IsGameOverCountdown()
    {
        return state == State.isGameOverCountdown;
    }

    public float GetGameOverCountdownTimer()
    {
        return gameOverCountdownTimer;
    }

    public bool IsGameOver()
    {
        return state == State.isGameOver;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;

            onGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;

            onGameUnpaused?.Invoke(this, EventArgs.Empty);

        }
    }
    public void ToggleOptions()
    {
        isOptions = !isOptions;
        if (isOptions)
        {

            onGameUnpaused?.Invoke(this, EventArgs.Empty);
            onOptionsShown?.Invoke(this, EventArgs.Empty);
        }
        else
        {

            onGamePaused?.Invoke(this, EventArgs.Empty);

            onOptionsNotShown?.Invoke(this, EventArgs.Empty);

        }
    }

}
