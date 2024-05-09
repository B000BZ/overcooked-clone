using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class RecipeSO : ScriptableObject    
{
    public EarnedSettings earnedSettings;

    public List<KitchenObjectSO> kitchenObjectSOList;
    public string recipeName;
    public Sprite recipeIcon;

}

[System.Serializable]

public struct EarnedSettings
{
    public Vector2 coinsEarned;
    public Vector3 timeInterval;
}