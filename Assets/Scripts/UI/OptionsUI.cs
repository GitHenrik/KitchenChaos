using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{

  public static OptionsUI Instance { get; private set; }

  [SerializeField] private Button soundEffectsButton;
  [SerializeField] private TextMeshProUGUI soundEffectsText;

  [SerializeField] private Button musicButton;
  [SerializeField] private TextMeshProUGUI musicText;

  [SerializeField] private Button returnButton;

  // buttons and texts for keybinds
  [SerializeField] private TextMeshProUGUI moveUpText;
  [SerializeField] private TextMeshProUGUI moveDownText;
  [SerializeField] private TextMeshProUGUI moveLeftText;
  [SerializeField] private TextMeshProUGUI moveRightText;
  [SerializeField] private TextMeshProUGUI interactText;
  [SerializeField] private TextMeshProUGUI alternateInteractText;
  [SerializeField] private TextMeshProUGUI pauseText;
  [SerializeField] private TextMeshProUGUI gamePadInteractText;
  [SerializeField] private TextMeshProUGUI gamePadAlternateInteractText;
  [SerializeField] private TextMeshProUGUI gamePadPauseText;

  [SerializeField] private Button moveUpButton;
  [SerializeField] private Button moveDownButton;
  [SerializeField] private Button moveLeftButton;
  [SerializeField] private Button moveRightButton;
  [SerializeField] private Button interactButton;
  [SerializeField] private Button alternateInteractButton;
  [SerializeField] private Button pauseButton;
  [SerializeField] private Button gamePadInteractButton;
  [SerializeField] private Button gamePadAlternateInteractButton;
  [SerializeField] private Button gamePadPauseButton;

  [SerializeField] private Transform pressToRebindKeyTransform;

  private Action onCloseButtonAction;

  private void Awake()
  {
    Instance = this;
    soundEffectsButton.onClick.AddListener(() =>
    {
      SoundManager.Instance.ChangeVolume();
      UpdateVisual();
    });
    musicButton.onClick.AddListener(() =>
    {
      MusicManager.Instance.ChangeVolume();
      UpdateVisual();
    });
    returnButton.onClick.AddListener(() =>
    {
      Hide();
      onCloseButtonAction();
    });
    moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up); });
    moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
    moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
    moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
    interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
    alternateInteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Alternate_Interact); });
    pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
    gamePadInteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePad_Interact); });
    gamePadAlternateInteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePad_Alternate_Interact); });
    gamePadPauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePad_Pause); });
  }

  private void Start()
  {
    KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
    UpdateVisual();
    Hide();
    HideRebind();
  }

  private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
  {
    Hide();
  }

  private void UpdateVisual()
  {
    soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
    musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

    moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
    moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
    moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
    moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
    interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
    alternateInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Alternate_Interact);
    pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    gamePadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Interact);
    gamePadAlternateInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Alternate_Interact);
    gamePadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Pause);

  }

  public void Show(Action onCloseButtonAction)
  {
    this.onCloseButtonAction = onCloseButtonAction;
    gameObject.SetActive(true);
    soundEffectsButton.Select();
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }

  private void ShowRebind()
  {
    pressToRebindKeyTransform.gameObject.SetActive(true);
  }

  private void HideRebind()
  {
    pressToRebindKeyTransform.gameObject.SetActive(false);
  }

  private void RebindBinding(GameInput.Binding binding)
  {
    ShowRebind();
    GameInput.Instance.RebindBinding(binding, () =>
    {
      HideRebind();
      UpdateVisual();
    });
  }
}
