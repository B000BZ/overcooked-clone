using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{

    public static event EventHandler OnPlayerItemPickup;
    public static event EventHandler OnPlayerItemDrop;





    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                OnPlayerItemDrop?.Invoke(this, EventArgs.Empty);
                
            }
             
            
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                OnPlayerItemPickup?.Invoke(this, EventArgs.Empty);

            }
            else
            {
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredients(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        OnPlayerItemPickup?.Invoke(this, EventArgs.Empty);
                    }
                }
                else
                {
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                       if (plateKitchenObject.TryAddIngredients(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                            OnPlayerItemPickup?.Invoke(this, EventArgs.Empty);

                        }
                    }
                }
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        Debug.Log("interact_alternate");
    }

}

 