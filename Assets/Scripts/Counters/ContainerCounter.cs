using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    public  event EventHandler OnPlayerGrabbedObject;
    public static event EventHandler OnPlayerItemPickup;

     public static void ResetStaticData()
    {
        OnPlayerItemPickup = null;
    }


    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

                OnPlayerItemPickup?.Invoke(this, EventArgs.Empty);
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                

            }

        }

    }

    public override void InteractAlternate(Player player)
    {
        Debug.Log("interact_alternate");
    }
}
