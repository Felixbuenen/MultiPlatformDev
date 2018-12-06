using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputSystem : IInputSystem
{
  Queue<int> serializedInput;
  public Queue<int> GetSerializedTrickInput()
  {
    return serializedInput;
  }

  public void Update() { }

  public Vector2 AnalogMoveInput()
  {
    Debug.Log("MOBILE NOT IMPLEMENTED");
    return Vector2.zero;
  }

  public Vector2 AnalogTrickInput()
  {
    Debug.Log("MOBILE NOT IMPLEMENTED");
    return Vector2.zero;
  }

}
