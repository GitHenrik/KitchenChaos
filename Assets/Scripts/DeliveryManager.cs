using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

  public static DeliveryManager Instance { get; private set; }

  [SerializeField] private RecipeListSO recipeListSO;
  private List<RecipeSO> waitingRecipeSOList;
  private float spawnRecipeTimer;
  private float spawnRecipeTimerMax = 4f;
  private int waitingRecipesMax = 4;

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

      if (waitingRecipeSOList.Count < waitingRecipesMax)
      {
        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
        Debug.Log(waitingRecipeSO.recipeName);
        waitingRecipeSOList.Add(waitingRecipeSO);
      }
    }
  }

  public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
  {
    for (int i = 0; i < waitingRecipeSOList.Count; i++)
    {
      RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

      if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
      {
        // Correct amount of ingredients
        bool plateContentsMatchesRecipe = true;
        foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
        {
          bool ingredientFound = false;
          foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
          {
            // for each ingredients in each recipe, check if the plate has the same ingredient
            if (plateKitchenObjectSO == recipeKitchenObjectSO)
            {
              ingredientFound = true;
              break;
            }
          }
          if (!ingredientFound)
          {
            // a required ingredient was missing from the plate
            plateContentsMatchesRecipe = false;
          }
        }
        if (plateContentsMatchesRecipe)
        {
          // all ingredients matched: correct recipe delivered
          Debug.Log("Nice, dish delivered!");
          waitingRecipeSOList.RemoveAt(i);
          return;
        }
      }
    }
    // Incorrect dish delivered
    Debug.Log("Delivery failed...");
  }

}
