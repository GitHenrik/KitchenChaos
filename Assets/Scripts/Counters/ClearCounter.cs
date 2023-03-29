using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  public override void Interact(Player player)
  {
    if (!HasKitchenObject())
    {
      // no kitchenObject on table
      if (player.HasKitchenObject())
      {
        // interacting player has something
        player.GetKitchenObject().SetKitchenObjectParent(this);
        //player.ClearKitchenObject(); not required, handled by SetKitchenObjectParent
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
        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
          // player is holding a plate, try adding an ingredient
          if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
          {
            // ingredient successfully added: destroy object on this counter
            GetKitchenObject().DestroySelf();
          }
        }
        else
        {
          // player is not carrying a Plate but is carrying something else
          if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
          {
            // Counter is holding a plate
            if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
            {
              // can add the ingredient that the player is holding onto the plate
              player.GetKitchenObject().DestroySelf();
            }
          }

        }
      }
      else
      {
        // player does not have anything
        GetKitchenObject().SetKitchenObjectParent(player);
      }
    }
  }
}
