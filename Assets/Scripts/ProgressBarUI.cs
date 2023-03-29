using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
  [SerializeField] private GameObject hasProgressGameObject;
  [SerializeField] private Image barImage;

  private IHasProgress hasProgress;

  private void Start()
  {
    // Note: Unity has no way of knowing that a gameObject implements a certain interface
    hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
    if (hasProgress == null)
    {
      Debug.LogError("Game Object " + hasProgressGameObject + " does not implement interface IProgress");
    }
    // listen to the event
    hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
    barImage.fillAmount = 0f;
    // Disable object AFTER the event handler starts listening.
    Hide();
  }

  private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
  {
    barImage.fillAmount = e.progressNormalized;

    if (e.progressNormalized == 0f || e.progressNormalized == 1f)
    {
      Hide();
    }
    else
    {
      Show();
    }
  }

  private void Show()
  {
    gameObject.SetActive(true);
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }
}
