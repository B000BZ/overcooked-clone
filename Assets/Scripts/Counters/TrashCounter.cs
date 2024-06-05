using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnPlayerItemDrop;

    public static void ResetStaticData()
    {
        OnPlayerItemDrop = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnPlayerItemDrop?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void InteractAlternate(Player player)
    {
        Debug.Log("interact_alternate");
    }

}
