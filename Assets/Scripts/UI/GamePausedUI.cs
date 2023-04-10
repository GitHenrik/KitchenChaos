using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
  [SerializeField] private Button mainMenuButton;
  [SerializeField] private Button resumeButton;
  [SerializeField] private Button optionsButton;

  private void Awake()
  {
    resumeButton.onClick.AddListener(() =>
    {
      KitchenGameManager.Instance.TogglePauseGame();
    });
    mainMenuButton.onClick.AddListener(() =>
    {
      Loader.Load(Loader.Scene.MainMenuScene);
    });
    optionsButton.onClick.AddListener(() =>
    {
      Hide();
      OptionsUI.Instance.Show(Show);
    });
  }

  private void Start()
  {
    KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
    KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
    Hide();
  }

  private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
  {
    Hide();
  }

  private void KitchenGameManager_OnGamePaused(object sender, EventArgs e)
  {
    Show();
  }

  private void Show()
  {
    gameObject.SetActive(true);
    resumeButton.Select();
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }
}
