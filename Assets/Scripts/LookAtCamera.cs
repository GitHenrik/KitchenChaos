using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
  private enum Mode
  {
    LookAt,
    LookAtInverted,
    CameraForward,
    CameraForwardInverted,
  }

  [SerializeField] private Mode mode;
  // LateUpdate useful in e.g. world canvases
  private void LateUpdate()
  {
    // in previous Unity versions Camera.main reference wasn't cached
    // no performance issues anymore, as the reference is cached
    switch (mode)
    {
      case Mode.LookAt:
        transform.LookAt(Camera.main.transform);
        break;
      case Mode.LookAtInverted:
        // direction pointing from the camera: subtract positions
        Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
        // look from self to camera
        transform.LookAt(transform.position + dirFromCamera);
        break;
      case Mode.CameraForward:
        transform.forward = Camera.main.transform.forward;
        break;
      case Mode.CameraForwardInverted:
        transform.forward = -Camera.main.transform.forward;
        break;
    }

  }
}
