using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

  public event EventHandler OnPlayerGrabbedObject;

  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  public override void Interact(Player player)
  {
    if (!player.HasKitchenObject()) // check that the player is not holding anything
    {
      KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
      OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
  }
}
