using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
  [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
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
      }
      else
      {
        // player does not have anything
        GetKitchenObject().SetKitchenObjectParent(player);
      }
    }
  }

  public override void InteractAlternate(Player player)
  {
    if (HasKitchenObject())
    {
      GetKitchenObject().DestroySelf();
      KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
    }
  }
}
