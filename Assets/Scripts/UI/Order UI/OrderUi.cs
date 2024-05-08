using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUi : MonoBehaviour
{
    [SerializeField] private Image _dishIcon;
    [SerializeField] private Image[] _ingrediantsIcon;

    [SerializeField] private RectTransform[] _ingrediantsParent;

    public void Init(RecipeSO recipe)
    {
        _dishIcon.sprite = recipe.recipeIcon;

        for (int i = 0; i < recipe.kitchenObjectSOList.Count; i++)
        {
            _ingrediantsIcon[i].sprite = recipe.kitchenObjectSOList[i].sprite;
            _ingrediantsParent[i].gameObject.SetActive(true);
        }
    }

    public void OrderDone()
    {
        //Remove order from list & Destroy it
        OrdersUiManager.instance.RemoveOrder(this);
    }
}
