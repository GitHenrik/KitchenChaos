using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  private IKitchenObjectParent kitchenObjectParent;

  public KitchenObjectSO GetKitchenObjectSO()
  {
    return kitchenObjectSO;
  }

  public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
  {
    // if previous parent exists, reset that parent's state to NOT have anything on it
    if (this.kitchenObjectParent != null)
    {
      this.kitchenObjectParent.ClearKitchenObject();
    }
    // update the parent reference to the new parent
    this.kitchenObjectParent = kitchenObjectParent;

    if (kitchenObjectParent.HasKitchenObject())
    {
      Debug.LogError("IKitchenObjectParent already has a KitchenObject!");
    }

    kitchenObjectParent.SetKitchenObject(this);

    // update visuals
    transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
    transform.localPosition = Vector3.zero;
  }

  public IKitchenObjectParent GetKitchenObjectParent()
  {
    return kitchenObjectParent;
  }

  public void DestroySelf()
  {
    kitchenObjectParent.ClearKitchenObject();
    Destroy(gameObject);
  }

  public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
  {
    Transform cutKitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
    KitchenObject kitchenObject = cutKitchenObjectTransform.GetComponent<KitchenObject>();
    kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
    return kitchenObject;
  }
}
