using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

  public static GameInput Instance { get; private set; }

  // instantiate event publisher
  public event EventHandler OnInteractAction;
  public event EventHandler OnInteractAlternateAction;
  public event EventHandler OnPauseAction;

  private PlayerInputActions playerInputActions;

  private void Awake()
  {
    Instance = this;
    playerInputActions = new PlayerInputActions();
    playerInputActions.Player.Enable();

    // When subscribing to an event, use only function reference, not a function call
    // e.g. "Interact_performed is fired when player interaction action is performed"
    playerInputActions.Player.Interact.performed += Interact_performed;
    playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    playerInputActions.Player.Pause.performed += Pause_performed;
  }

  private void OnDestroy()
  {
    // unsubscribe from all events and clear references to the previous playerInputActions instance
    playerInputActions.Player.Interact.performed -= Interact_performed;
    playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
    playerInputActions.Player.Pause.performed -= Pause_performed;

    playerInputActions.Dispose();
  }

  private void Pause_performed(InputAction.CallbackContext obj)
  {
    OnPauseAction?.Invoke(this, EventArgs.Empty);
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
