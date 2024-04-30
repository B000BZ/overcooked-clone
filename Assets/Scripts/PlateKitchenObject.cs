using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    [SerializeField] private Transform counterTopPoint;


    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public void AddIngredients(KitchenObjectSO kitchenObjectSO)
    {
        KitchenObject kitchenObject = kitchenObjectSO.prefab.GetComponent<KitchenObject>();


    }

    public bool TryAddIngredients(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }

        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }

    }



}
