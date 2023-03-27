using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
  [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
  public override void Interact(Player player)
  {
    if (!HasKitchenObject())
    {
      // no kitchenObject on table
      if (player.HasKitchenObject())
      {
        // interacting player has something
        if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
        {
          // Player has something that CAN be cut
          player.GetKitchenObject().SetKitchenObjectParent(this);
          //player.ClearKitchenObject(); not required, handled by SetKitchenObjectParent
        }
        else
        {
          // Player has something that CAN NOT be cut
        }
      }
      else
      {
        // nothing on the table, nothing on player: do nothing
      }
    }
    else
    {
      // there is a kitchenObject
      if (player.HasKitchenObject())
      {
        // player has something
      }
      else
      {
        // player does not have anything
        GetKitchenObject().SetKitchenObjectParent(player);
      }
    }
  }

  private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
  {
    foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
    {
      if (cuttingRecipeSO.input == kitchenObjectSO)
      {
        return true;
      }
    }
    return false;
  }

  public override void InteractAlternate(Player player)
  {
    if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
    {
      // There is an object that CAN be cut
      KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
      GetKitchenObject().DestroySelf();

      KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
    }
  }

  // Check recipes for an output for the current object on the cutting counter
  private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
  {
    foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
    {
      if (cuttingRecipeSO.input == inputKitchenObjectSO)
      {
        return cuttingRecipeSO.output;
      }
    }
    return null;
  }
}
