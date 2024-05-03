using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);


                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }

        }

    }

    public override void InteractAlternate(Player player)
    {
        Debug.Log("interact_alternate");
    }
}
