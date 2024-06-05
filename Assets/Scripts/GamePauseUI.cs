using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    


    private void Awake()
    {
        
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePause();
            
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenu);
        });
        
    }

    private void Start()
    {
        GameManager.Instance.onGamePaused += GameManager_onGamePaused;
        GameManager.Instance.onGameUnpaused += GameManager_onGameUnpaused;

        Hide();

    }

    private void GameManager_onGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_onGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
