using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnChangedStateEventArgs> OnChangedState;

    public class OnChangedStateEventArgs: EventArgs
    {
        public State state;
    }

    public enum State
    {
        idle,
        frying,
        fried,
        burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

       private State state;
       private float fryingProgress;
       private float burningProgress;

       private FryingRecipeSO fryingRecipeSO;
       private BurningRecipeSO burningRecipeSO;


    private void Start()
    {
        state = State.idle;     
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.idle:
                    Debug.Log("idle!");


                    break;
                case State.frying:
                    fryingProgress += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingProgress / fryingRecipeSO.fryingTimerMax

                    });
                    if (fryingProgress > fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        
                        state = State.fried;
                        burningProgress = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnChangedState?.Invoke(this, new OnChangedStateEventArgs{
                            state = state
                        });
                    }
                    break;
                    
                case State.fried:
                    burningProgress += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningProgress / burningRecipeSO.burningTimerMax

                    });
                    if (burningProgress > burningRecipeSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.burned;

                        OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.burned:
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f

                    }) ;
                    break;

            }
                
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeForInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);                    

                    fryingRecipeSO = GetFryingRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.frying;
                    fryingProgress = 0f;

                    OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingProgress / fryingRecipeSO.fryingTimerMax

                    }) ;

                }
                else if (HasBurningRecipeForInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.fried;
                    burningProgress = 0f;

                    OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f

                    });



                }
                else
                {
                    //player not carrying anything
                }

            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.idle;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f

                }); 

                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state
                });
            }
            else
            {
                // player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredients(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.idle;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f

                        });

                        OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                        {
                            state = state
                        });
                    }
                }
            }
        }
    }


    private bool HasRecipeForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }
    private bool HasBurningRecipeForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BurningRecipeSO burningRecipeSO = GetBurningRecipeSOWithInput(inputKitchenObjectSO);
        return burningRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

}   
