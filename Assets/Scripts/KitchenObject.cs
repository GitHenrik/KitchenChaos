using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;
  private ClearCounter clearCounter;

  public KitchenObjectSO GetKitchenObjectSO()
  {
    return kitchenObjectSO;
  }

  public void SetClearCounter(ClearCounter clearCounter)
  {
    // if previous counter exists, reset that counter's state to NOT have anything on it
    if (this.clearCounter != null)
    {
      this.clearCounter.ClearKitchenObject();
    }
    // update the counter reference to the new counter
    this.clearCounter = clearCounter;

    if (clearCounter.HasKitchenObject())
    {
      Debug.LogError("Counter already has a KitchenObject!");
    }

    clearCounter.SetKitchenObject(this);

    // update visuals
    transform.parent = clearCounter.GetKitchenObjectFollowTransform();
    transform.localPosition = Vector3.zero;
  }

  public ClearCounter GetClearCounter()
  {
    return clearCounter;
  }
}
