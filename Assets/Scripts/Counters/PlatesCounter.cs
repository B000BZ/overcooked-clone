using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter 
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateTaken;
    public static event EventHandler OnPlayerItemPickup;


    [SerializeField] private KitchenObjectSO platesKitchenObjectSO;


    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int spawnedPlatesAmount;
    private int spawnedPlatesAmountMax = 4;


    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0;
            if (spawnedPlatesAmount < spawnedPlatesAmountMax)
            {
                spawnedPlatesAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }


    }

    public override void Interact(Player player)
    {
        if (spawnedPlatesAmount > 0)
        {

            if (!player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(platesKitchenObjectSO, player);

                spawnedPlatesAmount--;

                OnPlateTaken?.Invoke(this, EventArgs.Empty);
                OnPlayerItemPickup?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        Debug.Log("interact_alternate");
    }

}
