using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

  public static SoundManager Instance { get; private set; }

  [SerializeField] private AudioClipRefsSO audioClipRefsSO;
  private float volume = 1f;

  private void Awake()
  {
    Instance = this;
    volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
  }

  private void Start()
  {
    DeliveryManager.Instance.OnRecipeFailure += DeliveryManager_OnRecipeFailure;
    DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
    CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
    Player.Instance.OnPickedSomething += Player_OnPickedSomething;
    BaseCounter.OnAnyObjectPlacement += BaseCounter_OnAnyObjectPlacement;
    TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
  }

  private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
  {
    TrashCounter trashCounter = sender as TrashCounter;
    PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
  }

  private void BaseCounter_OnAnyObjectPlacement(object sender, EventArgs e)
  {
    BaseCounter baseCounter = sender as BaseCounter;
    PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
  }

  private void Player_OnPickedSomething(object sender, EventArgs e)
  {
    PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
  }

  private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
  {
    CuttingCounter cuttingCounter = sender as CuttingCounter;
    PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
  }

  private void DeliveryManager_OnRecipeFailure(object sender, EventArgs e)
  {
    // could play sounds exactly on top of the camera, which has an audio listener attached
    // these sound effects were designed to play somewhere in the world and are loud:
    // play on top of the delivery counter instead
    DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
    PlaySound(audioClipRefsSO.deliveryFailure, deliveryCounter.transform.position);
  }

  private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
  {
    DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
    PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
  }

  private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
  {
    PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volumeMultiplier);
  }

  private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
  {
    AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
  }

  public void PlayFootstepSound(Vector3 position, float volumeMultiplier = 1f)
  {
    PlaySound(audioClipRefsSO.footstep, position, volumeMultiplier);
  }

  public void ChangeVolume()
  {
    volume += .1f;
    if (volume > 1f)
    {
      volume = 0f;
    }
    // save preferred values using PlayerPrefs
    PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
    // Save() not necessarily needed, sanity check in case unity crashes before auto save
    PlayerPrefs.Save();
  }

  public float GetVolume()
  {
    return volume;
  }
}
