using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutorialUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI upText;
  [SerializeField] private TextMeshProUGUI leftText;
  [SerializeField] private TextMeshProUGUI downText;
  [SerializeField] private TextMeshProUGUI rightText;
  [SerializeField] private TextMeshProUGUI interactKeyboardText;
  [SerializeField] private TextMeshProUGUI altKeyboardText;
  [SerializeField] private TextMeshProUGUI pauseKeyboardText;
  [SerializeField] private TextMeshProUGUI interactGamepadText;
  [SerializeField] private TextMeshProUGUI altGamepadText;
  [SerializeField] private TextMeshProUGUI pauseGamepadText;

  private void Start()
  {
    GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
    KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
    UpdateVisual();
    Show();
  }

  private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
  {
    if (KitchenGameManager.Instance.IsCountdownToStartActive())
    {
      Hide();
    }
  }

  private void GameInput_OnBindingRebind(object sender, EventArgs e)
  {
    UpdateVisual();
  }

  private void Show()
  {
    gameObject.SetActive(true);
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }

  private void UpdateVisual()
  {
    upText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
    leftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
    downText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
    rightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
    interactKeyboardText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
    altKeyboardText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Alternate_Interact);
    pauseKeyboardText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    interactGamepadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Interact);
    altGamepadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Alternate_Interact);
    pauseGamepadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Pause);
  }
}
