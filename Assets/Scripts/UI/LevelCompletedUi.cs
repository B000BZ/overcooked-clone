using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelCompletedUi : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI LevelCompletedText;
    [SerializeField] private Image LevelCompletedBackground;
    [SerializeField] private GameObject holder;

    private void Start()
    {
        //GameManager.Instance.onChangedState += GameManager_onChangedState;
        Hide();
    }

    private void Update()
    {
       
        if (DeliveryManager.Instance.GetCompletedRecipes())
        {
            
            Show();
        }
        else
        {
            Hide();
        }
    }

    //private void GameManager_onChangedState(object sender, System.EventArgs e)
    //{
    //    if ((GameManager.Instance.IsGameOver() && DeliveryManager.Instance.GetCompletedRecipes() )|| DeliveryManager.Instance.GetCompletedRecipes())
    //    {
    //        Show();
    //    }
    //    else
    //    {
    //        Hide();
    //    }
    //}

    private void Show()
    {
        holder.SetActive(true);
    }

    private void Hide()
    {
        holder.SetActive(false);
    }


}
