using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountdownUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;


    private void Start()
    {
        GameManager.Instance.onChangedState += GameManager_onChangedState;

    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownTimer()).ToString();
    }

    private void GameManager_onChangedState(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdown())
        {
            Show();
        }
        else
        {
            Hide();
        }
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
