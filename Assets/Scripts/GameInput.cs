using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
  // instantiate event publisher
  public event EventHandler OnInteractAction;
  public event EventHandler OnInteractAlternateAction;
  private PlayerInputActions playerInputActions;

  private void Awake()
  {
    playerInputActions = new PlayerInputActions();
    playerInputActions.Player.Enable();

    // When subscribing to an event, use only function reference, not a function call
    // e.g. "Interact_performed is fired when player interaction action is performed"
    playerInputActions.Player.Interact.performed += Interact_performed;
    playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
  }

  private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    // GameInput instantiation: "pressing F does something" -> 
    // Player: describes what "something" is by setting value for OnInteractAlternateAction
    OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
  }

  private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    // check for NPE: ValueToCheck?.Invoke()
    OnInteractAction?.Invoke(this, EventArgs.Empty);
  }

  public Vector2 GetMovementVectorNormalized()
  {
    Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

    inputVector = inputVector.normalized;

    return inputVector;
  }
}
