using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                Destroy(player.GetKitchenObject().gameObject);

            }
        }

    }

    public override void InteractAlternate(Player player)
    {
        Debug.Log("interact alternate");
    }
}
