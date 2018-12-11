using System.Collections;
using UnityEngine;

public class TrickExecuter : MonoBehaviour
{

  void OnGUI()
  {
    GUILayout.Label("x acceleration: " + Input.acceleration.x);
  }

}
