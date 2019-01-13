using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{

  PS4InputSystem inputSystem;
  PS4InputSystem InputSystem
  {
    get { return inputSystem; }
  }

  // Use this for initialization
  public InputManager()
  {
    //base input system on build
    inputSystem = new PS4InputSystem();

  }

}
