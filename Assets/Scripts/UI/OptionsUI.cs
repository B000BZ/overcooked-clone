using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        
        optionsButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleOptions();
        });
        backButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleOptions();
        });
        sfxSlider.onValueChanged.AddListener((float value) =>
        {
        SoundManager.Instance.ChangeVolume(sfxSlider.value);
        });
        musicSlider.onValueChanged.AddListener((float value) =>
        {
           MusicManager.Instance.ChangeVolume(musicSlider.value);
        });



    }

    private void Start()
    {
        GameManager.Instance.onOptionsShown += GameManager_onOptionsShown;
        GameManager.Instance.onOptionsNotShown += GameManager_onOptionsNotShown;

        Hide();
    }

    private void GameManager_onOptionsNotShown(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_onOptionsShown(object sender, System.EventArgs e)
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
