using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
  [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

  private List<KitchenObjectSO> kitchenObjectSOList;

  private void Awake()
  {
    kitchenObjectSOList = new List<KitchenObjectSO>();
  }

  public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
  {
    // check for invalid recipe ingredients
    if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
    {
      return false;
    }
    if (kitchenObjectSOList.Contains(kitchenObjectSO))
    {
      // already includes this ingredient
      return false;
    }
    else
    {
      kitchenObjectSOList.Add(kitchenObjectSO);
      return true;
    }
  }
}
