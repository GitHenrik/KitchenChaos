using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

  // PascalCase: not a field, but a property
  public static Player Instance { get; private set; }

  public event EventHandler OnPickedSomething;
  public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
  public class OnSelectedCounterChangedEventArgs : EventArgs
  {
    public BaseCounter selectedCounter;
  }

  [SerializeField] private float moveSpeed = 7f;
  [SerializeField] private GameInput gameInput;
  [SerializeField] private LayerMask countersLayerMask;
  [SerializeField] private Transform KitchenObjectHoldPoint;

  private bool isWalking;
  private Vector3 lastInteractDir;
  private BaseCounter selectedCounter;
  private KitchenObject kitchenObject;

  private void Awake()
  {
    if (Instance != null)
    {
      Debug.LogError("More than one Player instance");
    }
    Instance = this;
  }

  private void Start()
  {
    // Player sets values for event handlers that were instantiated on GameInput.Awake()
    // describe methods for fired actions
    gameInput.OnInteractAction += GameInput_OnInteractAction;
    gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
  }

  private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
  {
    if (!KitchenGameManager.Instance.IsGamePlaying()) return;

    if (selectedCounter != null)
    {
      selectedCounter.InteractAlternate(this);
    }


  }

  private void GameInput_OnInteractAction(object sender, System.EventArgs e)
  {
    if (!KitchenGameManager.Instance.IsGamePlaying()) return;

    if (selectedCounter != null)
    {
      selectedCounter.Interact(this);
    }
  }

  private void Update()
  {
    HandleMovement();
    HandleInteractions();
  }

  public bool IsWalking()
  {
    return isWalking;
  }

  private void HandleInteractions()
  {
    Vector2 inputVector = gameInput.GetMovementVectorNormalized();
    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

    if (moveDir != Vector3.zero)
    {
      lastInteractDir = moveDir;
    }

    float interactDistance = 2f;
    // RaycastHit: Structure used to get information back from a raycast
    // Variables passed as "out" arguments do not have to be initialized before being passed in a method call
    // .Raycast has several useful overloads for different situations
    if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
    {
      // tryGetComponent performs null check (vs. GetComponent)
      if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
      {
        //clearCounter.Interact();
        if (baseCounter != selectedCounter)
        {
          SetSelectedCounter(baseCounter);
        }
      }
      else
      {
        SetSelectedCounter(null);
      }
    }
    else
    {
      SetSelectedCounter(null);
    }
  }

  private void HandleMovement()
  {
    Vector2 inputVector = gameInput.GetMovementVectorNormalized();

    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
    float playerRadius = .7f;
    float playerHeight = 2f;
    float moveDistance = moveSpeed * Time.deltaTime;
    bool canMove = !Physics.CapsuleCast(
      transform.position,
      transform.position + Vector3.up * playerHeight,
      playerRadius,
      moveDir,
      moveDistance);

    if (!canMove)
    {
      // cannot move towards this direction
      // Attempt only X
      Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
      // if moving diagonally, and there is nothing along X-axis to block it, player can move
      canMove = moveDir.x != 0 && !Physics.CapsuleCast(
        transform.position,
        transform.position + Vector3.up * playerHeight,
        playerRadius,
        moveDirX,
        moveDistance
      );
      if (canMove)
      {
        // can move only on X
        moveDir = moveDirX;
      }
      else
      {
        // cannot move X, attempt Z
        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
        // if moving diagonally, and there is nothing along Z-axis to block it, player can move

        canMove = moveDir.z != 0 && !Physics.CapsuleCast(
          transform.position,
          transform.position + Vector3.up * playerHeight,
          playerRadius,
          moveDirZ,
          moveDistance
        );
        if (canMove)
        {
          // can move only on X
          moveDir = moveDirZ;
        }
        else
        {
          // cannot move in any direction
        }
      }
    }
    if (canMove)
    {

      transform.position += moveDir * moveDistance;
    }
    isWalking = moveDir != Vector3.zero;
    float rotateSpeed = 10f;
    transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
  }

  private void SetSelectedCounter(BaseCounter selectedCounter)
  {
    this.selectedCounter = selectedCounter;
    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
    {
      selectedCounter = selectedCounter
    });
  }

  public Transform GetKitchenObjectFollowTransform()
  {
    return KitchenObjectHoldPoint;
  }

  public void SetKitchenObject(KitchenObject kitchenObject)
  {
    // Play possible item grab animation here
    this.kitchenObject = kitchenObject;
    if (kitchenObject != null)
    {
      OnPickedSomething?.Invoke(this, EventArgs.Empty);
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
