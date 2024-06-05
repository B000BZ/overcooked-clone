using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int spawnedRecipeMax = 0;
    private int completedRecipes = 0;
    private int uncompletedRecipes = 0;


    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFail;



    private void Awake()
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipeMax && spawnedRecipeMax < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                spawnedRecipeMax++;

                OrdersUiManager.instance.DisplayOrder(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        RecipeSO waitingRecipeSO = waitingRecipeSOList[0];

        // Vérifie si le nombre d'ingrédients de l'assiette correspond à celui de la recette attendue
        if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
        {
            bool plateContentMatchesRecipe = true;

            // Parcours tous les ingrédients de la recette attendue
            foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
            {
                bool ingredientFound = false;

                // Parcours tous les ingrédients de l'assiette
                foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                {
                    // Vérifie si l'ingrédient de l'assiette correspond à celui de la recette
                    if (plateKitchenObjectSO == recipeKitchenObjectSO)
                    {
                        ingredientFound = true;
                        break;
                    }
                }

                // Si un ingrédient de la recette n'est pas trouvé dans l'assiette, la commande échoue
                if (!ingredientFound)
                {
                    plateContentMatchesRecipe = false;
                    break;
                }
            }

            // Si tous les ingrédients de la recette sont présents dans l'assiette, la commande est réussie
            if (plateContentMatchesRecipe)
            {
                // Met à jour les pièces gagnées par le joueur
                CoinsUi.instance.UpdateCoins(OrdersUiManager.instance.GetCoinsEarned(OrdersUiManager.instance._orders[0]));

                // Retire la recette livrée de la liste des recettes en attente
                waitingRecipeSOList.RemoveAt(0);

                // Indique à l'interface utilisateur que la commande est terminée avec succès
                OrdersUiManager.instance.OrderDone(OrdersUiManager.instance._orders[0]);
                completedRecipes++;
                waitingRecipeMax++;

                OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                

                return;
            }
        }

        // Si la commande échoue (ingrédients incorrects ou nombre incorrect d'ingrédients), retire la recette de la liste
        waitingRecipeSOList.RemoveAt(0);
        OrdersUiManager.instance.OrderFail(OrdersUiManager.instance._orders[0]);
        uncompletedRecipes++;
        OnDeliveryFail?.Invoke(this, EventArgs.Empty);

        return;
    }

    public bool GetCompletedRecipes()
    {
        return completedRecipes >= waitingRecipeMax;
    }

    public int GetUncompletedRecipes()
    {
        return uncompletedRecipes;
    }

    public bool IsFinishedRecipes()
    {
        return waitingRecipeMax == spawnedRecipeMax;
    }


}


