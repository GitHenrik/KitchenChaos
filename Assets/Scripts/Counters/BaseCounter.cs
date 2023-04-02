using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
  public static event EventHandler OnAnyObjectPlacement;

  public static void ResetStaticData()
  {
    OnAnyObjectPlacement = null;
  }

  [SerializeField] private Transform counterTopPoint;

  private KitchenObject kitchenObject;

  // overridden in extending classes. Should never be fired from base class.
  public virtual void Interact(Player player)
  {
    Debug.LogError("BaseCounter.Interact();");
  }

  public virtual void InteractAlternate(Player player)
  {
    //Debug.LogError("BaseCounter.InteractAlternate();");
  }

  public Transform GetKitchenObjectFollowTransform()
  {
    return counterTopPoint;
  }

  public void SetKitchenObject(KitchenObject kitchenObject)
  {
    this.kitchenObject = kitchenObject;
    if (kitchenObject != null)
    {
      OnAnyObjectPlacement?.Invoke(this, EventArgs.Empty);
    }
  }

  public KitchenObject GetKitchenObject()
  {
    return kitchenObject;
  }

  public void ClearKitchenObject()
  {
    kitchenObject = null;
  }

  public bool HasKitchenObject()
  {
    return kitchenObject != null;
  }
}
