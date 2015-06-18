using UnityEngine;
using System.Collections;

public class HideLockCursor : MonoBehaviour
{
  void Update()
  {
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
  }
}