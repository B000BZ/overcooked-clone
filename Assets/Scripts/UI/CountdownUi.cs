using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountdownUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI gameOverCountdownText;

    private void Start()
    {
        GameManager.Instance.onChangedState += GameManager_onChangedState;

        HideCountDownText();
        HideGameOverCountDownText();
    }

    private void Update()
    {
        countDownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownTimer()).ToString();
        gameOverCountdownText.text = Mathf.Ceil(GameManager.Instance.GetGameOverCountdownTimer()).ToString();

    }

    private void GameManager_onChangedState(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdown())
        {
            ShowCountDownText();
        }
        else
        {
            HideCountDownText();
        }
        if (GameManager.Instance.IsGameOverCountdown() && !DeliveryManager.Instance.GetCompletedRecipes())
        {
            ShowGameOverCountDownText();
        }
        else
        {
            HideGameOverCountDownText();
        }
    }



    private void ShowCountDownText()
    {
        countDownText.gameObject.SetActive(true);
    }

    private void HideCountDownText()
    {
        countDownText.gameObject.SetActive(false);
    }


    private void ShowGameOverCountDownText()
    {
        gameOverCountdownText.gameObject.SetActive(true);
    }

    private void HideGameOverCountDownText()
    {
        gameOverCountdownText.gameObject.SetActive(false);
    }

}
